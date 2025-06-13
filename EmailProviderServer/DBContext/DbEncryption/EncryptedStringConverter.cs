using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.DbEncryption
{
    using EmailProviderServer.Helpers;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class EncryptedStringConverter : ValueConverter<string, string>
    {
        public EncryptedStringConverter() : base(
            v => EncryptionHelper.Encrypt(v),
            v => EncryptionHelper.Decrypt(v))
        { }
    }

}
