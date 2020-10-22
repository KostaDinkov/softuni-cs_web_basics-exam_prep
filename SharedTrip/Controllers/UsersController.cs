using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using SUS.MvcFramework.ViewEngine;

namespace SharedTrip.Controllers
{
    class UsersController : Controller
    {

        private readonly UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.usersService.GetUserId(input.Username, input.Password);

            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }
            
            this.SignIn(userId);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords must match");
            }

            if (!usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email not available");
            }

            if (!usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username not available");
            }

            var userId = usersService.Create(input.Username, input.Password, input.Email);
            
            this.SignIn(userId);
            
            return Redirect("/Trips/All");
        }

        
    }
}
