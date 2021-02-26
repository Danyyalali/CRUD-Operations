using CRUD_OPERATION.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUD_OPERATION.Controllers
{
    public class StudentController : ApiController
    {

        
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// To get all the students record available in the database
        /// </summary>
        /// <returns></returns>
        
        [Route("Api/GetAll")]
        public IHttpActionResult GetAllStudents()
        {
            IList<StudentViewModel> students = null;

            using (var context = new ManagementContext())
            {
                students = context.student.Include("StudentAddress")
                    .Select(s => new StudentViewModel()
                            {
                                Id = s.Id,
                                FirstName = s.FirstName,
                                LastName = s.LastName
                            }).ToList<StudentViewModel>();
            }

            if (students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="IncludeAddress"></param>
        /// <returns></returns>
        //Get all students either with address or without it.
        [Route("Api/GetAll/Address")]
        public IHttpActionResult GetAllStudentsWithAddress(Boolean IncludeAddress)
        {
            ICollection<StudentViewModel> students = null;
            using(var context = new ManagementContext())
            {
                students = context.student.Include("StudentAddress")
                    .Select(s => new StudentViewModel()
                    {
                        Id=s.Id,
                        FirstName = s.FirstName,
                        LastName=s.LastName,
                        StudentAddress=IncludeAddress==false? null: new AddressViewModel()
                        {
                            Id=s.StudentAddress.Id,
                            StudentId=s.StudentAddress.StudentId,
                            Address1=s.StudentAddress.Address1,
                            Address2=s.StudentAddress.Address2,
                            city=s.StudentAddress.city,
                            state=s.StudentAddress.state
                        }
                    }).ToList<StudentViewModel>();
            }
            if (students == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(students);
            }
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Get student by id
        [Route("Api/Get/Id")]
        public IHttpActionResult GetStudentByid(int id)
        {
            StudentViewModel student = null;
            using(var context = new ManagementContext())
            {
                student = context.student.Include("StudentAddress")
                    .Where(s=>s.Id==id)
                    .Select(s => new StudentViewModel()
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName
                    }).ToList<StudentViewModel>().FirstOrDefault<StudentViewModel>();
            }
            if (student == null)
            {
                return NotFound();
            }
            else
                return Ok(student);
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //Get students by name
        
        [Route("Api/Get/Name")]
        public IHttpActionResult GetStudentByName(string name)
        {
            StudentViewModel student = null;
            using (var context = new ManagementContext())
            {
                student = context.student.Include("StudentAddress")
                    .Where(s => (s.FirstName == name || s.LastName == name))
                    .Select(s => new StudentViewModel()
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName
                    }).ToList<StudentViewModel>().FirstOrDefault<StudentViewModel>();
            }
            if (student == null)
            {
                return NotFound();
            }
            else
                return Ok(student);
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Get students by checking wether name contains that string or not
        /// </summary>
        /// <param name="NameString"></param>
        /// <returns></returns>

        [Route("Api/Get/Characters")]
        public IHttpActionResult GetStudentByNameString(string NameString)
        {
            StudentViewModel student = null;
            using (var context = new ManagementContext())
            {
                student = context.student.Include("StudentAddress")
                    .Where(s => (s.FirstName.Contains(NameString) || s.LastName.Contains(NameString)))
                    .Select(s => new StudentViewModel()
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName
                    }).ToList<StudentViewModel>().FirstOrDefault<StudentViewModel>();
            }
            if (student == null)
            {
                return NotFound();
            }
            else
                return Ok(student);
        }

        [Route("api/Post/StudentData")]
        public IHttpActionResult PostStudent(StudentViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input data.");

            using (var ctx = new ManagementContext())
            {
                ctx.student.Add(new Student()
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    cgpa=student.cgpa,
                    email=student.email,
                    StudentAddress = new StudentAddress()
                    {
                        Address1 = student.StudentAddress.Address1,
                    Address2 = student.StudentAddress.Address2,
                    city = student.StudentAddress.city,
                    state = student.StudentAddress.state
                    }
                });
                

                ctx.SaveChanges();
               /* ctx.address.Add(new StudentAddress()
                {
                    
                });*/
            }

            return Ok(200);
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Update the User Data Which is already present in the database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [Route("api/Update/StudentData")]
        public IHttpActionResult PutStudent(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is inValid");
            }
            Student existingStudent=null;
            StudentAddress existingStudentAddress = null;
            using (var context = new ManagementContext())
            {
                existingStudent = context.student.Where(s => s.Id == student.Id).FirstOrDefault<Student>();
                existingStudentAddress = context.address.Where(a => a.StudentId == student.Id).FirstOrDefault<StudentAddress>();
                if (existingStudent == null)
                {
                    return BadRequest("The student with ID does not exist in our record Please register or provide Valid Id");
                }
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.cgpa = student.cgpa;
                existingStudent.email = student.email;

                existingStudentAddress.Address1 = student.StudentAddress.Address1;
                existingStudentAddress.Address2 = student.StudentAddress.Address2;
                existingStudentAddress.city = student.StudentAddress.city;
                existingStudentAddress.state= student.StudentAddress.state;

                context.SaveChanges();

            }
            return Ok(200);

        }


        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// To delete the data from the database by getting the id as an input
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/Delete/Student")]
        public IHttpActionResult DeleteStudent(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide valid arguments");
            }
            Student StudentToBeDeleted = null;
            StudentAddress StudentAddressToBeDeleted = null;
            using (var context= new ManagementContext())
            {
                try
                {
                    StudentToBeDeleted = context.student.Where(s => s.Id == id).FirstOrDefault<Student>();
                    StudentAddressToBeDeleted = context.address.Where(a => a.StudentId == id).FirstOrDefault<StudentAddress>();
                }
                catch
                {
                    return BadRequest("Something Went Wrong");
                }
                if(StudentToBeDeleted==null || StudentAddressToBeDeleted == null)
                {
                    return BadRequest("The ID you enetered does not exist in our Database");
                }
                context.Entry(StudentToBeDeleted).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.Entry(StudentAddressToBeDeleted).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChanges();
                return Ok(200);
            }

        }




    }

}
