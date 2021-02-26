using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_OPERATION
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float cgpa { get; set; }
        public string email { get; set; }
        public virtual StudentAddress StudentAddress { get; set; }
        public ICollection<StuCourse> StuCourses { get; set; }


    }
}