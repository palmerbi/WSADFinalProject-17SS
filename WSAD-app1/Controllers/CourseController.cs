using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_app1.Models.Data;
using WSAD_app1.Models.ViewModels.Courses;

namespace WSAD_app1.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        [Authorize]
        public ActionResult Index()
        {
            List<CourseViewModel> courseVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                courseVM = context.Course.ToArray()
                    .Select(x => new CourseViewModel(x))
                    .ToList();
            }
                return View(courseVM);
        }
    }
}