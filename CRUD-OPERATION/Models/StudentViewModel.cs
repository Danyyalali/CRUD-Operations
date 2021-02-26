using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_OPERATION.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float cgpa { get; set; }
        public string email { get; set; }
        public AddressViewModel StudentAddress { get; set; }
        public StuCourseViewModel StuCourse { get; set; }
    }
}