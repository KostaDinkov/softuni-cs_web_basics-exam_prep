using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SharedTrip.Models;

namespace SharedTrip.Services
{
    public class UsersService: IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string GetUserId(string username, string password)
        {
            var hashedPassword = ComputeHash(password);
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
            return user?.Id;

        }
        
        public string Create(string username, string password, string email)
        {
            var hashedPassword = ComputeHash(password);
            var newUser = new User() {Email = email, Password = hashedPassword, Username = username};
            db.Users.Add(newUser);
            db.SaveChanges();
            return newUser.Id;
        }

        public bool IsUsernameAvailable(string username)
        {
            return !db.Users.Any(u => u.Username == username);
        }

        public bool IsEmailAvailable(string email)
        {
            return !db.Users.Any(u => u.Email == email);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}
