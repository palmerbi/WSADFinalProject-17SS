using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WSAD_app1.Models.ViewModels.CourseSearch
{
    [DataContract]
    public class CourseSearchViewModel
    {
        public CourseSearchViewModel(Data.Course courseDTO)
        {
            this.Id = courseDTO.Id;
            this.Name = courseDTO.Name;
            this.TeacherName = courseDTO.TeacherName;
            this.MeetingDates = courseDTO.MeetingDates;
            this.MeetingTime = courseDTO.MeetingTime;
            this.Occupancy = courseDTO.Occupancy;
        }
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string TeacherName { get; private set; }
        [DataMember]
        public string MeetingDates { get; private set; }
        [DataMember]
        public DateTime MeetingTime { get; private set; }
        [DataMember]
        public int Occupancy { get; private set; }
    }
}