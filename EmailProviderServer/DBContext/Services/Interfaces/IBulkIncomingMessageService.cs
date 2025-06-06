﻿//Includes

using EmailServiceIntermediate.Models;

namespace EmailProviderServer.DBContext.Services.Base
{
    //------------------------------------------------------
    //	IBulkIncomingMessageService
    //------------------------------------------------------
    public interface IBulkIncomingMessageService
    {
        int GetCount();

        IEnumerable<BulkIncomingMessage> GetAll(int? nCount = null);
    }
}
