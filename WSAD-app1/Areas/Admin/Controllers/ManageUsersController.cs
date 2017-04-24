using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_app1.Areas.Admin.Models.ViewModels.ManageUser;
using WSAD_app1.Models.Data;

namespace WSAD_app1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        // GET: ManageUsers
        public ActionResult Index()
        {
            //Setup a DbContext
            List<ManageUserViewModel> collectionOfUsersVMs = new List<ManageUserViewModel>();
            using (WSADDbContext context = new WSADDbContext())
            {
                //get all users
                var dbUsers = context.Users;

                //move users into viewmodel object
                foreach (var userDTO in dbUsers)
                {
                    collectionOfUsersVMs.Add(new ManageUserViewModel(userDTO));
                }
            }
            //send viewmodel collection to the view
            return View(collectionOfUsersVMs);
        }

        [HttpPost]
        public ActionResult Delete(List<ManageUserViewModel> collectionOfUserVM)
        {
            var vmItemsToDelete = collectionOfUserVM.Where(x => x.IsSelected == true);
            using (WSADDbContext context = new WSADDbContext())
            {
                foreach (var vmIitem in vmItemsToDelete)
                {
                    var dtoToDelete = context.Users.FirstOrDefault(row => row.Id == vmIitem.Id);
                    context.Users.Remove(dtoToDelete);
                }
                context.SaveChanges();
            }

            return RedirectToAction("index");
        }

        public ActionResult DeleteCourse(List<UserDetailsViewModel> detailsVM)
        {
            var vmItemsToDelete = detailsVM.Where(x => x.IsSelected == true);
            using (WSADDbContext context = new WSADDbContext())
            {
                foreach (var vmItem in vmItemsToDelete)
                {
                    var dtoToDelete = context.ShoppingCart.FirstOrDefault(row => row.UserId == vmItem.UserId && row.CourseId == vmItem.CourseID);
                    context.ShoppingCart.Remove(dtoToDelete);
                }
                context.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("index");
        }


        public ActionResult UserDetails(int usersID)
        {
            List<ShoppingCart> courseDTO;
            List<UserDetailsViewModel> userDetailsVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                courseDTO = context.ShoppingCart.Where(row => row.UserId == usersID)
                    .ToList();
            }
            userDetailsVM = courseDTO.Select(row => new UserDetailsViewModel(row)).ToList();

            return View(userDetailsVM);
        }

        public ActionResult AddCourseToList(int userId, int courseId)
        {
            //verify input parameters
            if (userId <= 0 || courseId <= 0)
            {
                return this.HttpNotFound("Invalid input parameters");
            }

            using(WSADDbContext context = new WSADDbContext())
            {
                Course courseDTO = context.Course.FirstOrDefault(x => x.Id == courseId);
                User userDTO = context.Users.FirstOrDefault(x => x.Id == userId);

                if (courseDTO == null) { return this.HttpNotFound("Invalid Input Parameters"); }
                if (userDTO == null) { return this.HttpNotFound("Invalid Input Parameters"); }
                
                if(!context.ShoppingCart.Any(row => row.UserId == userId && row.CourseId == courseId))
                {
                    ShoppingCart newCartItem = new ShoppingCart()
                    {
                        UserId = userId,
                        CourseId = courseId
                    };
                    context.ShoppingCart.Add(newCartItem);
                }
                context.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("index");
        }
    }
}