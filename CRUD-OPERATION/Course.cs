using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_OPERATION
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string CreditHours { get; set; }
        public virtual Teacher teachers { get; set; }
        public int TeacherId { get; set; }
        public ICollection<StuCourse> StuCourses { get; set; }

    }
}