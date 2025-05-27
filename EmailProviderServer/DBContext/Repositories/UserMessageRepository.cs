using EmailProvider.Enums;
using EmailProviderServer.DBContext.DbHelpers;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class UserMessageRepository : IUserMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public UserMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetIncomingMessagesAsync(int userId, Expression<Func<Message, bool>>? filter, int skip, int take)
        {
            return await _context.UserMessages
                .Where(um => um.UserId == userId && !um.IsDeleted)
                .Include(um => um.Message)
                    .ThenInclude(m => m.MessageRecipients)
                .Select(um => um.Message)
                .Where(m => m.Direction == EmailDirections.EmailDirectionIn)
                .OrderByDescending(m => m.DateOfRegistration)
                .Where(filter ?? (_ => true))
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Message>> GetOutgoingMessagesAsync(int userId, Expression<Func<Message, bool>>? filter, int skip, int take)
        {
            return await _context.UserMessages
                .Where(um => um.UserId == userId && !um.IsDeleted)
                .Include(um => um.Message)
                    .ThenInclude(m => m.MessageRecipients)
                .Select(um => um.Message)
                .Where(m => m.Direction == EmailDirections.EmailDirectionOut)
                .OrderByDescending(m => m.DateOfRegistration)
                .Where(filter ?? (_ => true))
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Message>> GetDraftMessagesAsync(int userId, Expression<Func<Message, bool>>? filter, int skip, int take)
        {
            return await _context.UserMessages
                .Where(um => um.UserId == userId && !um.IsDeleted)
                .Include(um => um.Message)
                    .ThenInclude(m => m.MessageRecipients)
                .Select(um => um.Message)
                .Where(m => m.Direction == EmailDirections.EmailDirectionOut && m.Status == EmailStatuses.EmailStatusDraft)
                .OrderByDescending(m => m.DateOfRegistration)
                .Where(filter ?? (_ => true))
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Message>> GetMessagesInFolderAsync(int userId, int folderId, Expression<Func<Message, bool>>? filter, int skip, int take)
        {
            return await _context.UserMessages
                .Where(um => um.UserId == userId && um.FolderId == folderId && !um.IsDeleted)
                .Include(um => um.Message)
                    .ThenInclude(m => m.MessageRecipients)
                .Select(um => um.Message)
                .OrderByDescending(m => m.DateOfRegistration)
                .Where(filter ?? (_ => true))
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
