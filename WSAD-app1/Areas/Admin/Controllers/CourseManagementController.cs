using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_app1.Areas.Admin.Models.ViewModels.CourseManagemnt;
using WSAD_app1.Areas.Admin.Models.ViewModels.ManageUser;
using WSAD_app1.Models.Data;

namespace WSAD_app1.Areas.Admin.Controllers
{
    public class CourseManagementController : Controller
    {
        // GET: Course
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<CourseManagementViewModel> manageCourseVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                manageCourseVM = context.Course.ToArray()
                    .Select(x => new CourseManagementViewModel(x))
                    .ToList();
            }
            return View(manageCourseVM);
        }

        public ActionResult Delete(List<CourseManagementViewModel> courseManageVM)
        {
            var vmToDelete = courseManageVM.Where(x => x.IsSelected == true);
            using (WSADDbContext context = new WSADDbContext())
            {
                foreach (var vmItem in vmToDelete)
                {
                    var dtoToDelete = context.Course.FirstOrDefault(row => row.Id == vmItem.Id);
                    context.Course.Remove(dtoToDelete);
                }
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult CourseDetails(int courseID)
        {
            List<ShoppingCart> courseDTO;
            List<UserDetailsViewModel> courseVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                courseDTO = context.ShoppingCart.Where(row => row.CourseId == courseID)
                    .ToList();
            }
            courseVM = courseDTO.Select(row => new UserDetailsViewModel(row)).ToList();

            return View(courseVM);
        }
    }
}