using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WSAD_app1.Models.ViewModels.CreateUser
{
    public class CreateUserViewModel
    {
        [Display(Name = "First Name :")]
        public string FName { get; set; }
        [Display(Name = "Last Name :")]
        public string LName { get; set; }
        [Display(Name = "Email :")]
        public string EMail { get; set; }
        [Display(Name = "Userame :")]
        public string Username { get; set; }
        [Display(Name = "Password :")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password :")]
        public string PasswordConfirm { get; set; }
        [Display(Name = "Would you like to receive emails?")]
        public bool ReceiveEmail { get; set; }

        public string Gender { get; set; }

    }
}