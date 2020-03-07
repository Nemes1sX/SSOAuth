using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using SSOauth.Models;
using SSOauth.Services;
using SSOauth.Interfaces;
using SSOauth.Data;
using System.ComponentModel;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SSOauth.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private User _user;
        private Login _credentials;
        private readonly ApplicationSettings _appSettings;
        private readonly IUser db;


        public AuthController(IOptions<ApplicationSettings> appSettings, User user, Login credentials, IUser _db)
         {
            db = _db;
            //_user = user;
            _credentials = credentials;
             _appSettings = appSettings.Value;
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
        public int CreateUser([FromBody] User model)
        {
            string pepper = Service.RandomString(10);
            pepper = Service.sha256_hash(pepper);
            string sign = model.login + pepper + model.claim + Service.sha256_hash(model.password);
            var user = new User()
            {
                login = model.login,
                password = Service.sha256_hash(model.password),
                claim = model.claim,
                created_at = DateTime.UtcNow.AddHours(2),
                pepper = Service.sha256_hash(pepper),
                signature = Service.rsa_hash(sign)    
            };
            return db.Register(user);
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
