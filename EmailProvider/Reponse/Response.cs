using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailProvider.Reponse
{
    public class Response
    {
        public bool bSuccess { get; set; }
        public string msgError { get; set; }

        public object Data { get; set; }

        public Response()
        {
            bSuccess = true;
            msgError = string.Empty;
        }
    }
}
