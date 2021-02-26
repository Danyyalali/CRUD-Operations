using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_OPERATION
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public int Contactnumber { get; set; }
        public ICollection<Course> Courses { get; set; }

    }
}