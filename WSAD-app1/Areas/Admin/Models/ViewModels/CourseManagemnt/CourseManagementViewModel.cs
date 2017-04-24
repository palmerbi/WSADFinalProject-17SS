using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAD_app1.Areas.Admin.Models.ViewModels.CourseManagemnt
{
    public class CourseManagementViewModel
    {
        public CourseManagementViewModel()
        {

        }

        public CourseManagementViewModel(WSAD_app1.Models.Data.Course row)
        {
            this.Id = row.Id;
            this.Name = row.Name;
            this.TeacherName = row.TeacherName;
            this.MeetingDates = row.MeetingDates;
            this.MeetingTime = row.MeetingTime;
            this.Occupancy = row.Occupancy;
            this.IsActive = row.IsActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public string MeetingDates { get; set; }
        public DateTime MeetingTime { get; set; }
        public int Occupancy { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
    }
}