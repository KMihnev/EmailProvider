using EmailProvider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmailProvider.Dispatches
{
    public class MethodRequest
    {
        public DispatchEnums eDispatch { get; set; }
        public JsonElement Parameters { get; set; }
    }
}
