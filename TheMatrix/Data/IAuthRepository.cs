using TheMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMatrix.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(string user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}