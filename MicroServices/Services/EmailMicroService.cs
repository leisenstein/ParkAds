using DataAccess.ExternalServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.Services
{
    public class EmailMicroService
    {
        public void SendSimpleMessage()
        {
            EmailService.SendSimpleMessage();
        }
    }
}
