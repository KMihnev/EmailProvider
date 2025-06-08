//Includes
using EmailServiceIntermediate.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmailService.PrivateService
{
    //------------------------------------------------------
    //	DispatchMapper
    //------------------------------------------------------
    public class DispatchMapper
    {
        //Constructor
        public DispatchMapper()
        {
        }

        //Methods
        public BaseDispatchHandler MapDispatch(int dispatchCode)
        {
            switch ((DispatchEnums)dispatchCode)
            {
                case DispatchEnums.SendEmailToService: return new SendEmailDispatchService();
                default:
                    throw new NotImplementedException($"Dispatch code {dispatchCode} is not supported.");
            }
        }
    }
}
