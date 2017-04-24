using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_app1.Models.Data;
using WSAD_app1.Models.ViewModels.Courses;
using WSAD_app1.Models.ViewModels.ShoppingCart;

namespace WSAD_app1.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        [Authorize]
        public ActionResult Index()
        {
            List<ShoppingCartViewModel> shoppingCartItems;
            using (WSADDbContext context = new WSADDbContext())
            {
                //get user information
                string username = User.Identity.Name;
                //get user from dB
                int userID = context.Users
                    .Where(x => x.Username == username)
                    .Select(x => x.Id)
                    .FirstOrDefault();
                //get shopping cart items
                shoppingCartItems =
                    context.ShoppingCart.Where(x => x.UserId == userID)
                    .ToArray()
                    .Select(x => new ShoppingCartViewModel(x))
                    .ToList();

                //generate shopping cart viewmodel
            }
            return View(shoppingCartItems);
        }

        [HttpPost]
        public ActionResult AddToOrder(List<CourseViewModel> courseToAdd)
        {
            //Verify that projductsToAdd is not null
            if (courseToAdd == null) { return RedirectToAction("index"); }
            //capture products to add(filter by isselected)
            courseToAdd = courseToAdd.Where(p => p.IsSelected).ToList();
            //if no probles to add, tell the users, redriect back to shoppingcart index
            if (courseToAdd.Count <= 0) { return RedirectToAction("index"); }
            //get user from user.Identity
            string username = User.Identity.Name;


            using (WSADDbContext context = new WSADDbContext())
            {
                //get user from DB
                int userId = context.Users
                    .Where(row => row.Username == username)
                    .Select(row => row.Id)
                    .FirstOrDefault();

                foreach (CourseViewModel cVM in courseToAdd)
                {
                    //Does this product user combno exist
                    if (!context.ShoppingCart.Any(row =>
                     row.UserId == userId && row.CourseId == cVM.Id))
                    {

                        //Create a shoping cart DTO
                        ShoppingCart shoppingDTO = new ShoppingCart()
                        {
                            //add products to DTO
                            UserId = userId,
                            CourseId = cVM.Id
                        };

                        //Add DTO to DB Context
                        context.ShoppingCart.Add(shoppingDTO);
                    }
                }
                //Save DB Context, redirect to shopping cart index.
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int courseId, int userId)
        {
            using (WSADDbContext context = new WSADDbContext())
            {
                var toDelete = context.ShoppingCart.FirstOrDefault(row => row.UserId == userId && row.CourseId == courseId);
                context.ShoppingCart.Remove(toDelete);
                context.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
    }

    
}