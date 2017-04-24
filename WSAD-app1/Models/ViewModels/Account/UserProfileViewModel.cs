using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAD_app1.Models.ViewModels.Account
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAdmin { get; set; }
    }
}