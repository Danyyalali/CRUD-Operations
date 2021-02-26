using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_OPERATION.Models
{
    public class StuCourseViewModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public ICollection<StudentViewModel> Students { get; set; }
    }
}