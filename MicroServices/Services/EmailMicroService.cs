using DataAccess.ExternalServices;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace MicroServices.Services
{
    public class EmailMicroService
    {
        public void SendSimpleMessage(Ad ad, Object obj)
        {
            EmailService.SendSimpleMessage(ad,obj);
        }
    }
}
