using DataAccess.ExternalServices;
using DataTransferObjects;

namespace MicroServices.Services
{
    public class EmailMicroService
    {
        public void SendSimpleMessage(EmailTransferObject emailTransferObject)
        {
            EmailService.SendSimpleMessage(emailTransferObject);
        }
    }
}
