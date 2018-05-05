using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TheMatrix.Models;
using Microsoft.EntityFrameworkCore;
using TheMatrix.Data;

namespace TheMatrix.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            user.LastActive = DateTime.Now;
            _context.Users.Update(user);
            return (user == null || !PasswordHashVerified(password, user.PassHash, user.Salt)) ? null : user;
        }

        private bool PasswordHashVerified(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // Hash password and compare with PasswordHash stored in database
            var hash = new HMACSHA512();
            hash.Key = passwordSalt;
            var enteredHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            if (enteredHash.SequenceEqual(passwordHash))
                return true;
            else
                return false;
        }

        public async Task<User> Register(User user, string password)
        {
            // Hash the password using SHA512 with random key (salt)
            var hash = new HMACSHA512();
            var computedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            // var newUser = new User { UserName = user.UserName };
            // newUser.PassHash = computedHash;
            user.PassHash = computedHash;
            // newUser.Salt = hash.Key;
            user.Salt = hash.Key;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return (user == null) ? false : true;
        }
    }
}