using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_app1.Models.ViewModels.Courses;

namespace WSAD_app1.Models.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {

        }

        public ShoppingCartViewModel(Data.ShoppingCart row)
        {
            this.Id = row.ID;
            this.UserId = row.UserId;
            this.CourseId = row.CourseId;
            this.Course = new CourseViewModel(row.Course);

        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public CourseViewModel Course { get; set; }
    }
}