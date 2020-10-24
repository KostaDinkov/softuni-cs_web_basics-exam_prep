using BattleCards.Data;
using BattleCards.Validation;
using BattleCards.ViewModels.User;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BattleCards.Models;

namespace BattleCards.Services
{
    class UserService : IUserService
    {

        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public string GetUserId(string username, string password)
        {
            var hashedPassword = ComputeHash(password);
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
            return user?.Id;
        }

        public string Create(RegisterInputModel input)
        {
            var validationResult = ValidateRegisterInput(input);
            if (!validationResult.IsValid) 
                throw new InputValidationException(validationResult.Errors);
            
            User user = new User
            {
                Username = input.Username,
                Password = ComputeHash(input.Password),
                Email = input.Email,
            };

            db.Users.Add(user);
            db.SaveChanges();
            return user.Id;

        }
        private IValidationResult ValidateRegisterInput(RegisterInputModel input)
        {

            var result = new ValidationResult { IsValid = true };

            if (input.Username.Length < 5 || input.Username.Length > 20)
                SetError("Username must be between 5 and 20 characters long.");
            if (!RegexUtilities.IsValidEmail(input.Email))
                SetError("Email is not valid.");
            if (string.IsNullOrWhiteSpace(input.Password) || input.Password.Length < 6 || input.Password.Length > 20)
                SetError("Password cannot be empty and must be between 6 and 20 characters long.");
            if (input.Password != input.ConfirmPassword)
                SetError("Passwords do not match.");

            // before hitting the db
            if (!result.IsValid)
            {
                return result;
            }

            // validation dependent on db
            if (db.Users.Any(u => u.Email == input.Email))
                SetError("Email not available.");

            if (db.Users.Any(u => u.Username == input.Username))
                SetError("Username not available.");



            return result;
            void SetError(string error)
            {
                result.Errors.Add(error);
                result.IsValid = false;
            }
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