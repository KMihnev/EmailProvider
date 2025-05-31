using EmailProvider.Enums;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class UserMessageRepository : IUserMessageRepository
{
    private readonly ApplicationDbContext _context;

    public UserMessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserMessage>> GetIncomingMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take)
    {
        return await _context.UserMessages
            .Where(um => um.UserId == userId &&
                         !um.IsDeleted &&
                         um.Message.Direction == EmailDirections.EmailDirectionIn)
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Include(um => um.UserMessageFolders)
            .Where(filter ?? (_ => true))
            .OrderByDescending(um => um.Message.DateOfRegistration)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<List<UserMessage>> GetOutgoingMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take)
    {
        return await _context.UserMessages
            .Where(um => um.UserId == userId &&
                         !um.IsDeleted &&
                         um.Message.Direction == EmailDirections.EmailDirectionOut &&
                         um.Message.Status != EmailStatuses.EmailStatusDraft)
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Include(um => um.UserMessageFolders)
            .Where(filter ?? (_ => true))
            .OrderByDescending(um => um.Message.DateOfRegistration)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<List<UserMessage>> GetDraftMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take)
    {
        return await _context.UserMessages
            .Where(um => um.UserId == userId &&
                         !um.IsDeleted &&
                         um.Message.Direction == EmailDirections.EmailDirectionOut &&
                         um.Message.Status == EmailStatuses.EmailStatusDraft)
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Include(um => um.UserMessageFolders)
            .Where(filter ?? (_ => true))
            .OrderByDescending(um => um.Message.DateOfRegistration)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<List<UserMessage>> GetMessagesInFolderAsync(int userId, int folderId, Expression<Func<UserMessage, bool>>? filter, int skip, int take)
    {
        return await _context.UserMessages
            .Where(um => um.UserId == userId &&
                         !um.IsDeleted &&
                         um.UserMessageFolders.Any(f => f.FolderId == folderId))
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Include(um => um.UserMessageFolders)
            .Where(filter ?? (_ => true))
            .OrderByDescending(um => um.Message.DateOfRegistration)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}
