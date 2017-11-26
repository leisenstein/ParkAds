using DataAccess.ExternalServices;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Services
{
    public class SpotMicroService
    {
        private SpotService spotService = new SpotService();
        public async Task<IEnumerable<Spot>> GetAllAsync()
        {
            return await spotService.GetAll();
        }
    }
}
