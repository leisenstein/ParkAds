using DataAccess.ExternalServices;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServices.Services
{
    public class AdMicroService
    {
        private AdService adService = new AdService();
        public Ad Get()
        {
            return adService.Get().Result;
        }
    }
}
