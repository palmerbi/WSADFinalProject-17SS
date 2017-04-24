using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_app1.Models.Data;

namespace WSAD_app1.Areas.Admin.Models.ViewModels.ManageUser
{
    public class ManageUserViewModel
    {
        public ManageUserViewModel()
        {

        }

        public ManageUserViewModel(User userDTO)
        {
            Id = userDTO.Id;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            EmailAddress = userDTO.EmailAddress;
            Username = userDTO.Username;
            isActive = userDTO.IsActive;
            isAdmin = userDTO.IsAdmin;
            Gender = userDTO.Gender;
            DateCreated = userDTO.DateCreated;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsSelected { get; set; }
    }
}