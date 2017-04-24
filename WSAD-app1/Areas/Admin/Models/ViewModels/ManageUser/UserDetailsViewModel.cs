using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_app1.Areas.Admin.Models.ViewModels.CourseManagemnt;
using WSAD_app1.Models.Data;

namespace WSAD_app1.Areas.Admin.Models.ViewModels.ManageUser
{
    public class UserDetailsViewModel
    {
        public UserDetailsViewModel()
        {

        }

        public UserDetailsViewModel(ShoppingCart shoppingCartDTO)
        {
            Id = shoppingCartDTO.ID;
            UserId = shoppingCartDTO.UserId;
            CourseID = shoppingCartDTO.CourseId;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseID { get; set; }
        public bool IsSelected { get; set; }
    }
}