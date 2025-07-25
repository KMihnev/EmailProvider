﻿using EmailProviderServer.DBContext.Repositories.Base;
using EmailServiceIntermediate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories.Interfaces
{
    public interface IBulkIncomingMessagesRepositoryS : IRepositoryS<BulkIncomingMessage>
    {
        Task AddBulkIncomingMessageAsync(BulkIncomingMessage bulkMessage);
    }
}
