﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Newtonsoft.Json;
using DataTransferObjects;
using Gateway.Mapping;
using Domain;
using Microsoft.Extensions.Logging;
using System;

namespace Gateway.Controllers
{
    [Produces("application/json")]
    [Route("api/user/")]
    public class UserController : Controller
    {
        private UserMicroService userMicroService = new UserMicroService();
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
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
            _logger.LogInformation("User login ,"+ email + DateTime.UtcNow);
            return UserMapping.MapDomainToDTOObject(userMicroService.GetByEmail(email));
        }
        
        // POST: api/User
        [HttpPost]
        public ContentResult Post([FromBody] UserDTO userDTO)
        {
            User user = UserMapping.MapDTOToDomainObject(userDTO);
            _logger.LogInformation("User created" + user.Email + DateTime.UtcNow );
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
