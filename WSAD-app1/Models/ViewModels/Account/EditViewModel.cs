using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSAD_app1.Models.ViewModels.Account
{
    public class EditViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}