using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_app1.Models.Data
{
    [Table("TblCourses")]
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public string MeetingDates { get; set; }
        public DateTime MeetingTime { get; set; }
        public int Occupancy { get; set; }
        public bool IsActive { get; set; }
    }
}