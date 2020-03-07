using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSOauth.Models;

namespace SSOauth.Interfaces
{
    public interface IUser
    {
        int Register(User user);
    }
}
