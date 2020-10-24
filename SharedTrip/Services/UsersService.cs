using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SharedTrip.Models;
using SharedTrip.Validation;
using SharedTrip.ViewModels.Users;


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
        
        public string Create(RegisterInputModel input)
        {
            var validationResult = ValidateRegisterInput(input);
            if (validationResult.IsValid)
            {
                input.Password = ComputeHash(input.Password);
                var newUser = new User() { Email = input.Email, Password = input.Password, Username = input.Username };
                db.Users.Add(newUser);
                db.SaveChanges();
                return newUser.Id;
            }
            else
            {
                throw new InputValidationException(validationResult.Errors);
            }

            
        }

        private  IValidationResult ValidateRegisterInput(RegisterInputModel model)
        {
            var result = new ValidationResult { IsValid = true };
            if (model.Username.Length < 5)
            {
                result.Errors.Add("Username must be between 5 and 20 characters long.");
                result.IsValid = false;
            }

            if (!RegexUtilities.IsValidEmail(model.Email))
            {
                result.Errors.Add("Email is not valid.");
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                result.Errors.Add("Password cannot be empty.");
                result.IsValid = false;
            }
            if (model.Password != model.ConfirmPassword)
            {
                result.Errors.Add("Passwords do not match.");
                result.IsValid = false;
            }

            if (db.Users.Any(u => u.Email == model.Email))
            {
                result.Errors.Add("Email not available.");
                result.IsValid = false;
            }

            if (db.Users.Any(u => u.Username == model.Username))
            {
                result.Errors.Add("Username not available.");
                result.IsValid = false;
            }

            return result;
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
