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
        public static Expression<Func<Message, bool>> BuildExpression(
            List<SearchCondition> conditions,
            bool isIncoming = false,
            bool isOutgoing = false,
            bool isDraft = false)
        {
            ParameterExpression param = Expression.Parameter(typeof(Message), "m");
            Expression? baseExpr = null;

            foreach (var condition in conditions)
            {
                Expression? expr = null;

                switch (condition.SearchType)
                {
                    case SearchType.SearchTypeDate:
                        if (DateTime.TryParse(condition.SearchValue, out var date))
                        {
                            var dateProp = Expression.Property(param, nameof(Message.DateOfRegistration));
                            expr = (SearchTypeDate)condition.SearchSubType switch
                            {
                                SearchTypeDate.SearchTypeDateBefore => Expression.LessThan(dateProp, Expression.Constant(date)),
                                SearchTypeDate.SearchTypeDateAfter => Expression.GreaterThan(dateProp, Expression.Constant(date)),
                                _ => null
                            };
                        }
                        break;

                    case SearchType.SearchTypeEmail:
                        if (isIncoming)
                        {
                            var fromProp = Expression.Property(param, nameof(Message.FromEmail));
                            expr = Expression.Call(fromProp, nameof(string.Contains), null,
                                Expression.Constant(condition.SearchValue));
                        }
                        else if (isOutgoing)
                        {
                            var recipientsProp = Expression.Property(param, nameof(Message.MessageRecipients));
                            var recipientParam = Expression.Parameter(typeof(MessageRecipient), "r");
                            var emailProp = Expression.Property(recipientParam, nameof(MessageRecipient.Email));
                            var containsCall = Expression.Call(emailProp, nameof(string.Contains), null, Expression.Constant(condition.SearchValue));
                            var anyLambda = Expression.Lambda<Func<MessageRecipient, bool>>(containsCall, recipientParam);
                            expr = Expression.Call(typeof(Enumerable), nameof(Enumerable.Any), new[] { typeof(MessageRecipient) }, recipientsProp, anyLambda);
                        }
                        break;
                }

                if (expr != null)
                    baseExpr = baseExpr == null ? expr : Expression.AndAlso(baseExpr, expr);
            }

            return baseExpr != null
                ? Expression.Lambda<Func<Message, bool>>(baseExpr, param)
                : m => true;
        }
    }
}
