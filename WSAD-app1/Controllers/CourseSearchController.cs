using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WSAD_app1.Models.Data;
using WSAD_app1.Models.ViewModels.CourseSearch;

namespace WSAD_app1.Controllers
{
    public class CourseSearchController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<CourseSearchViewModel> Get(string term)
        {
            using(WSADDbContext context = new WSADDbContext())
            {
                IQueryable<Course> matches;
                List<CourseSearchViewModel> csVM = new List<CourseSearchViewModel>();

                if(string.IsNullOrWhiteSpace(term))
                {
                    matches = context.Course.AsQueryable();
                }
                else
                {
                    matches = context.Course
                        .Where(row => row.Name.StartsWith(term));
                }            

                foreach(var courseDTO in matches)
                {
                    csVM.Add(new CourseSearchViewModel(courseDTO));
                }

                return csVM;
            }
        }
    }
}