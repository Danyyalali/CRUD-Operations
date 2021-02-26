using CRUD_OPERATION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CRUD_OPERATION.Controllers
{
    public class StudController : Controller
    {
        // GET: Stud
        public ActionResult Index()
        {
            IEnumerable<StudentViewModel> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:9642/api/");
                //HTTP GET
                var responseTask = client.GetAsync("students");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<StudentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }
    }
}