using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WSAD_app1.Models.Data;
using WSAD_app1.Models.ViewModels.Account;
using WSAD_app1.Views.Account;

namespace WSAD_app1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }

        /// <summary>
        /// To create a user account for my application
        /// </summary>
        /// <returns>View result for the create</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(CreateUserViewModel NewUser)
        {
            //Validate the new user
            //check required fields are set
            if(!ModelState.IsValid)
            {
                return View(NewUser);
            }
            //check passwords match
            if(!NewUser.Password.Equals(NewUser.ConfirmPassword))
            {
                ModelState.AddModelError("", "Yours passwords do not match");
            }

            //create instance of DBContext
            using (WSADDbContext Context = new WSADDbContext())
            {
                //Make sure username is unique
                if(Context.Users.Any(row => row.Username.Equals(NewUser.Username)))
                {
                    ModelState.AddModelError("", "I'm sorry, the username '" + NewUser.Username + "' already exists");
                    NewUser.Username = "";
                    return View(NewUser);
                }
                //Create our user dto
                User newUserDTO = new Models.Data.User()
                {
                    FirstName = NewUser.FirstName,
                    LastName = NewUser.LastName,
                    EmailAddress = NewUser.EmailAddress,
                    IsActive = true,
                    IsAdmin = false,
                    Username = NewUser.Username,
                    Password = NewUser.Password,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Gender = NewUser.Gender
                };
                //Add to DBContext
                Context.Users.Add(newUserDTO);
                //Save changes
                Context.SaveChanges();
            }
            //redirect to login page
            return RedirectToAction("login");
        }

        /// <summary>
        /// Loggin users into the website
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginUserViewModel loginUser)
        {           

            //Validate username and password is passed
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Login in required");
                return View();
            }

            if(string.IsNullOrWhiteSpace(loginUser.Username))
            {
                ModelState.AddModelError("", "Username is required");
                return View();
            }

            if (string.IsNullOrWhiteSpace(loginUser.Password))
            {
                ModelState.AddModelError("", "Password is required");
                return View();
            }
            //Open database connection
            bool isValid = false;
            using (WSADDbContext context = new WSADDbContext())
            {
                //Hash password

                //Query for user based on username and password hash
                if(context.Users.Any(row => row.Username.Equals(loginUser.Username) && row.Password.Equals(loginUser.Password)))
                {
                    isValid = true;
                }              
            }
                //If invalid, send error
            if(!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
            else
            {
                //valid redirect to user profile
                System.Web.Security.FormsAuthentication.SetAuthCookie(loginUser.Username, loginUser.RememberMe);
                return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.Username, loginUser.RememberMe));
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        public ActionResult UserNavPartial()
        {
            //Capture logged in user
            string username;
            username = this.User.Identity.Name;
            //Get user info from db
            UserNavPartialViewModel userNavVM;

            using (WSADDbContext context = new WSADDbContext())
            {
                Models.Data.User userDTO = context.Users.FirstOrDefault(x => x.Username == username);
                if (userDTO == null) { return Content(""); }

                userNavVM = new UserNavPartialViewModel()
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Id = userDTO.Id
                };
            }
                //build our usernavpartialmodel

                //send the view model to the partial view

                return PartialView(userNavVM);
        }

        public ActionResult UserProfile(int? id = null)
        {
            //Capture Logged In User
            string username = User.Identity.Name;

            //Retreiwe User from database
            UserProfileViewModel profileVM;

            using (WSADDbContext context = new WSADDbContext())
            {
                //populate userprofileviewmodel
                User userDTO;
                if( id.HasValue)
                {
                    userDTO = context.Users.Find(id.Value);
                }
                else
                {
                    userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                }
                    

                if (userDTO == null)
                {
                    return Content("Invalid Username");
                }
                profileVM = new UserProfileViewModel()
                {
                    DateCreated = userDTO.DateCreated,
                    EmailAddress = userDTO.EmailAddress,
                    FirstName = userDTO.FirstName,
                    //Gender = userDTO.Gender,
                    Id = userDTO.Id,
                    IsAdmin = userDTO.IsAdmin,
                    LastName = userDTO.LastName,
                    Username = userDTO.Username

                };

            }
            //retrun view with viewmodel
            return View(profileVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get the user by ID
            EditViewModel editvm;

            using (WSADDbContext context = new WSADDbContext())
            {
                //create user by id
                User userDTO = context.Users.Find(id);

                //Create a editViewModel
                if(userDTO == null)
                {
                    return Content("Invalid ID");

                }
                editvm = new EditViewModel()
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Id = userDTO.Id,
                    EmailAddress = userDTO.EmailAddress,
                    Username = userDTO.Username,
                    Gender = userDTO.Gender
                };
            }
            return View(editvm);

            //SEnd the viewmodel to the view
        }
        [HttpPost]
        public ActionResult Edit(EditViewModel editVM)
        {
            //variables
            bool needsPasswordReset = false;
            bool usernameHasChanged = false;
            //Validate the model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }
            //check for a password change
            //Compare password with password confirm
            if(!string.IsNullOrWhiteSpace(editVM.Password))
            {
                if(editVM.Password != editVM.PasswordConfirm)
                {
                    ModelState.AddModelError("", "Password and Password Confirm must match");
                }
                else
                {
                    needsPasswordReset = true;
                }
            }
            //get our user from DB
            using (WSADDbContext context = new WSADDbContext())
            {
                User userDTO = context.Users.Find(editVM.Id);
                if (userDTO == null) {return Content("Invalid User ID");}
                if (userDTO.Username != editVM.Username)
                {
                    userDTO.Username = editVM.Username;
                    usernameHasChanged = true;
                }
                //Set/update values from VM
                userDTO.FirstName = editVM.FirstName;
                userDTO.DateModified = DateTime.Now;
                userDTO.EmailAddress = editVM.EmailAddress;
                userDTO.LastName = editVM.LastName;
                userDTO.Gender = editVM.Gender;
                
                if(needsPasswordReset)
                {
                    userDTO.Password = editVM.Password;
                }

                //Save changes
                context.SaveChanges();
            }
            if (usernameHasChanged || needsPasswordReset)
            {
                TempData["LogoutMessage"] = "After a username or password change, you must relog with the new credentials.";
                return RedirectToAction("Logout");
            }
            else
            { 
                return RedirectToAction("UserProfile");
            }
        }
    }
}