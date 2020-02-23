using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using SSOauth.Models;
using SSOauth.Services;
using SSOauth.Data;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SSOauth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private User _user;
        private Login _credentials;
        private readonly ApplicationSettings _appSettings;
        private SSOAuthContext _db;

        public AuthController(IOptions<ApplicationSettings> appSettings, User user, Login credentials, SSOAuthContext db)
         {
            _user = user;
            _credentials = credentials;
             _appSettings = appSettings.Value;
            _db = db;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Register")]
        public async Task<Object> CreateUser(User model)
        {
            string pepper = Service.RandomString(10);
            pepper = Service.sha256_hash(pepper);
            string signature = model.login + pepper + model.claim + Service.sha256_hash(model.password);
            signature = ByteConverter.GetBytes(signature);
            var user = new User()
            {
                login = model.login,
                created_at = DateTime.UtcNow.AddHours(2),
                password = Service.sha256_hash(model.password),
                
                claim = model.claim,
                signature = Service.rsa_hash(signature, RSA.ExportParameters(false), false)
            };
           var result =  await User.CreateAsync(user, Service.sha256_hash(model.password));
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
