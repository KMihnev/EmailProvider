﻿using EmailProvider.Enums;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class UserMessageRepository : IUserMessageRepository
{
    private readonly ApplicationDbContext _context;

    public UserMessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserMessage>> GetIncomingMessagesAsync(int userId, string userEmail, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy, string? keyword)
    {
        var query = _context.UserMessages
        .Where(um => um.UserId == userId && um.Message.MessageRecipients.Any(x=>x.Email == userEmail) && !um.UserMessageFolders.Any())
        .Include(um => um.Message)
            .ThenInclude(m => m.MessageRecipients)
        .Include(um => um.UserMessageFolders)
            .Where(filter ?? (_ => true));

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(um =>
                um.Message.Subject.Contains(keyword) ||
                um.Message.Body.Contains(keyword));
        }

        query = orderBy == OrderBy.OrderByAscending? query.OrderBy(um => um.Message.DateOfRegistration): query.OrderByDescending(um => um.Message.DateOfRegistration);

        return await query.Skip(skip).Take(take) .ToListAsync();
    }

    public async Task<List<UserMessage>> GetOutgoingMessagesAsync(int userId, string userEmail, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy, string? keyword)
    {
        var query = _context.UserMessages
            .Where(um => um.UserId == userId && um.Message.Direction == EmailDirections.EmailDirectionOut && um.Message.FromEmail == userEmail && um.Message.Status != EmailStatuses.EmailStatusDraft && !um.UserMessageFolders.Any())
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Include(um => um.UserMessageFolders)
                .Where(filter ?? (_ => true));

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(um =>
                um.Message.Subject.Contains(keyword) ||
                um.Message.Body.Contains(keyword));
        }

        query = orderBy == OrderBy.OrderByAscending ? query.OrderBy(um => um.Message.DateOfRegistration) : query.OrderByDescending(um => um.Message.DateOfRegistration);

        return await query.Skip(skip).Take(take).ToListAsync();
    }
    public async Task<List<UserMessage>> GetDraftMessagesAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy, string? keyword)
    {
        var query = _context.UserMessages
            .Where(um => um.UserId == userId && !um.IsDeleted && um.Message.Direction == EmailDirections.EmailDirectionOut && um.Message.Status == EmailStatuses.EmailStatusDraft)
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Include(um => um.UserMessageFolders)
            .Where(filter ?? (_ => true));

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(um =>
                um.Message.Subject.Contains(keyword) ||
                um.Message.Body.Contains(keyword));
        }

        query = orderBy == OrderBy.OrderByAscending ? query.OrderBy(um => um.Message.DateOfRegistration) : query.OrderByDescending(um => um.Message.DateOfRegistration);

        return await query.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<UserMessage>> GetMessagesInFolderAsync(int userId, int folderId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy, string? keyword)
    {
        var query = _context.UserMessages
            .Where(um => um.UserId == userId && !um.IsDeleted && um.UserMessageFolders.Any(f => f.FolderId == folderId))
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Include(um => um.UserMessageFolders)
            .Where(filter ?? (_ => true));

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(um =>
                um.Message.Subject.Contains(keyword) ||
                um.Message.Body.Contains(keyword));
        }

        query = orderBy == OrderBy.OrderByAscending ? query.OrderBy(um => um.Message.DateOfRegistration) : query.OrderByDescending(um => um.Message.DateOfRegistration);

        return await query.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<List<UserMessage>> GetDeletedMessagesForUserAsync(int userId, Expression<Func<UserMessage, bool>>? filter, int skip, int take, OrderBy orderBy, string? keyword)
    {
        var query = _context.UserMessages
            .Where(um => um.UserId == userId &&
                         um.IsDeleted)
            .Include(um => um.Message)
                .ThenInclude(m => m.MessageRecipients)
            .Where(filter ?? (_ => true));

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(um =>
                um.Message.Subject.Contains(keyword) ||
                um.Message.Body.Contains(keyword));
        }

        query = orderBy == OrderBy.OrderByAscending ? query.OrderBy(um => um.Message.DateOfRegistration) : query.OrderByDescending(um => um.Message.DateOfRegistration);

        return await query.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<UserMessage> GetByUserAndMessageIdAsync(int userId, int messageId)
    {
        return await _context.UserMessages
            .FirstOrDefaultAsync(um => um.UserId == userId && um.MessageId == messageId);
    }

    public async Task SetIsDeletedAsync(int userId, int messageId, bool isDeleted)
    {
        var entry = await GetByUserAndMessageIdAsync(userId, messageId);
        if (entry != null)
        {
            entry.IsDeleted = isDeleted;

            if (isDeleted)
            {
                var foldersToRemove = _context.UserMessageFolders
                    .Where(umf => umf.UserMessageId == entry.Id);

                _context.UserMessageFolders.RemoveRange(foldersToRemove);
            }

            await _context.SaveChangesAsync();
        }
    }

    public async Task SetIsReadAsync(int userId, int messageId, bool isRead)
    {
        var entry = await GetByUserAndMessageIdAsync(userId, messageId);
        if (entry != null)
        {
            entry.IsRead = isRead;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> MoveMessagesToFolderAsync(List<int> messageIds, int folderId, int userId)
    {
        var messages = await _context.UserMessages
            .Include(m => m.UserMessageFolders)
            .Where(m => messageIds.Contains(m.MessageId))
            .ToListAsync();

        if (!messages.Any()) return false;

        foreach (var msg in messages)
        {
            if (msg.UserId != userId)
                continue;

            msg.UserMessageFolders.Clear();

            msg.UserMessageFolders.Add(new UserMessageFolder
            {
                FolderId = folderId,
                UserMessageId = msg.Id
            });
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveFromFolderAsync(int userId, List<int> messageIds)
    {
        var userMessages = await _context.UserMessages
            .Where(um => um.UserId == userId && messageIds.Contains(um.MessageId))
            .Include(um => um.UserMessageFolders)
            .ToListAsync();

        if (!userMessages.Any()) return false;

        foreach (var um in userMessages)
        {
            if (um.UserId != userId)
                continue;

            um.UserMessageFolders.Clear();
        }

        await _context.SaveChangesAsync();
        return true;
    }

}
