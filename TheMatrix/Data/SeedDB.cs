using TheMatrix.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMatrix.Data;

namespace MyMatrix.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            // remove existing users
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();

            // seed users
            var userData = System.IO.File.ReadAllText("Data/UserData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                // make password hash
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PassHash = passwordHash;
                user.Salt = passwordSalt;
                user.UserName = user.UserName.ToLower();
                _context.Users.Add(user);
            }
            _context.SaveChanges();

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
