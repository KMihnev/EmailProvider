using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EmailProvider.Enums;
using EmailProvider.SearchData;
using EmailServiceIntermediate.Enums;
using EmailServiceIntermediate.Models;

namespace EmailProviderServer.Helpers
{
    public static class SearchExpressionBuilder
    {
        public static Expression<Func<UserMessage, bool>> BuildExpression(
            List<SearchCondition> conditions,
            bool isIncoming = false,
            bool isDraft = false)
        {
            ParameterExpression param = Expression.Parameter(typeof(UserMessage), "um");
            var messageProp = Expression.Property(param, nameof(UserMessage.Message));
            Expression? baseExpr = null;

            foreach (var condition in conditions)
            {
                Expression? expr = null;

                switch (condition.SearchType)
                {
                    case SearchType.SearchTypeDate:
                    {
                        if (DateTime.TryParse(condition.SearchValue, out DateTime date))
                        {
                                var dateProp = Expression.Property(messageProp, nameof(Message.DateOfRegistration));
                                var hasValue = Expression.Property(dateProp, "HasValue");
                                var value = Expression.Property(dateProp, "Value");

                                Expression? dateExpr = (SearchTypeDate)condition.SearchSubType switch
                                {
                                    SearchTypeDate.SearchTypeDateBefore => Expression.LessThan(value, Expression.Constant(date)),
                                    SearchTypeDate.SearchTypeDateAfter => Expression.GreaterThan(value, Expression.Constant(date)),
                                    _ => null
                                };

                                expr = dateExpr != null ? Expression.AndAlso(hasValue, dateExpr) : null;
                            }
                        break;
                    }
                    case SearchType.SearchTypeEmail:
                    {
                        if (isIncoming)
                        {
                            var fromProp = Expression.Property(messageProp, nameof(Message.FromEmail));
                            expr = Expression.Call(fromProp, nameof(string.Contains), null,
                                Expression.Constant(condition.SearchValue));
                        }
                        else
                        {
                            var recipientsProp = Expression.Property(messageProp, nameof(Message.MessageRecipients));
                            var recipientParam = Expression.Parameter(typeof(MessageRecipient), "r");
                            var emailProp = Expression.Property(recipientParam, nameof(MessageRecipient.Email));
                            var containsCall = Expression.Call(emailProp, nameof(string.Contains), null, Expression.Constant(condition.SearchValue));
                            var anyLambda = Expression.Lambda<Func<MessageRecipient, bool>>(containsCall, recipientParam);
                            expr = Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new[] { typeof(MessageRecipient) }, recipientsProp, anyLambda);
                        }
                        break;
                    }
                    case SearchType.SearchTypeDeleted:
                    {
                        var isDeletedProp = Expression.Property(param, nameof(UserMessage.IsDeleted));
                        expr = Expression.Equal(isDeletedProp, Expression.Constant(true));
                        break;
                    }
                    case SearchType.SearchTypeRead:
                    {
                        var isReadProp = Expression.Property(param, nameof(UserMessage.IsRead));
                        expr = Expression.Equal(isReadProp, Expression.Constant(true));
                        break;
                    }
                    case SearchType.SearchTypeUnread:
                    {
                        var isReadProp = Expression.Property(param, nameof(UserMessage.IsRead));
                        expr = Expression.Equal(isReadProp, Expression.Constant(false));
                        break;
                    }
                }

                if (expr != null)
                    baseExpr = baseExpr == null ? expr : Expression.AndAlso(baseExpr, expr);
            }

            bool hasDeletedCondition = conditions.Any(c => c.SearchType == SearchType.SearchTypeDeleted);

            if (!hasDeletedCondition)
            {
                var isDeletedProp = Expression.Property(param, nameof(UserMessage.IsDeleted));
                var notDeletedExpr = Expression.Equal(isDeletedProp, Expression.Constant(false));
                baseExpr = baseExpr == null ? notDeletedExpr : Expression.AndAlso(baseExpr, notDeletedExpr);
            }

            return baseExpr != null
                ? Expression.Lambda<Func<UserMessage, bool>>(baseExpr, param)
                : um => true;
        }
    }
}
