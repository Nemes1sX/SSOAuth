using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSOauth.Interfaces;
using SSOauth.Models;
using Microsoft.EntityFrameworkCore;


namespace SSOauth.Data
{
    public class UserDataLayer : IUser
    {
        private SSOAuthContext db;
        public UserDataLayer(SSOAuthContext _db)
        {
            db = _db;
        }
        public int Register(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();

            return 1;
        }
    }
}
