using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Newtonsoft.Json;
using DataTransferObjects;
using Gateway.Mapping;
using Domain;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/user/")]
    public class UserController : Controller
    {
        private UserMicroService userMicroService = new UserMicroService();
        // GET: api/User
        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            return UserMapping.MapDomainToDTOCollection(userMicroService.GetAll());
        }
        
        [Route("id/{id}")]
        public UserDTO GetById(int id)
        {
            return UserMapping.MapDomainToDTOObject(userMicroService.GetById(id));
        }
        
        [Route("email/{email}")]
        public UserDTO GetByEmail(string email)
        {
            return UserMapping.MapDomainToDTOObject(userMicroService.GetByEmail(email));
        }
        
        // POST: api/User
        [HttpPost]
        public ContentResult Post([FromBody] UserDTO userDTO)
        {
            User user = UserMapping.MapDTOToDomainObject(userDTO);
            if (userMicroService.Add(user))
                return new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(user),
                    ContentType = "application/json",
                    StatusCode = 200
                };
            else
                return new ContentResult()
                {
                    StatusCode = 412
                };
        }
        
        // PUT: api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/api/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
