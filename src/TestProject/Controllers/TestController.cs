using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using TestProject.Models;
using System.IO;
using Newtonsoft.Json;

namespace TestProject.Controllers
{
    public class TestController : Controller
    {

        TestDbContext context = null;
        IHostingEnvironment env = null;
        public TestController(IHostingEnvironment _env, TestDbContext _context)
        {
            context = _context;
            env = _env;
        }
        public IActionResult First()
        {
            return View();
        }

        [HttpGet]
        public  IActionResult AddStudent()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult AddStudent( Test T)
        {


            foreach(var file in Request.Form.Files)
            {

                string name = file.Name;
                // get extension of file
                string ext = System.IO.Path.GetExtension(file.FileName);
                //NAME OF FILE stored with data time hour second name
                string filename = DateTime.Now.ToString("ddMMyyyyhhmmss").ToString() + ext;
                string path = "";
                if(name.Equals("Profile"))
                {
                    T.Profile = filename;
                    //path = env.WebRootPath + "data/pp/" + filename;
                    path = env.WebRootPath + "/data/pp/" + filename;

                }
                else if(name.Equals("Cv"))
                {

                    T.Cv = filename;
                    //path = env.WebRootPath + "data/cv/" + filename;
                    path = env.WebRootPath + "/data/cv/" + filename;
                }

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fs);
                }



            }


            context.Test.Add(T);
            context.SaveChanges();
            return View();
        }

        public IActionResult ListStudent()
        {
            IList<Test> all = context.Test.ToList<Test>();
            return View(all);
        }
        public FileResult Download(string filepath)
        {
            string ext = System.IO.Path.GetExtension(filepath);
            return File("/data/cv/" + filepath, System.Net.Mime.MediaTypeNames.Application.Octet, "Download" + ext);

                 


        }

        public IActionResult ListOfStudents()
        {
            IList<Test> studentsList = context.Test.ToList<Test>();

            return View(studentsList);
        }

        public FileResult DownloadFile(string filePath)
        {
            string ext = Path.GetExtension(filePath);


            return File("~/data/cv/" + filePath, System.Net.Mime.MediaTypeNames.Application.Octet, "Download" + ext);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {

            }
            //Student S = context.Student.Where(m => m.Id == 5).FirstOrDefault<Student>();
            Test test= context.Test.Where(t => t.Id == id).SingleOrDefault<Test>();

            return View(test);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {

            }
            Test Del = context.Test.Where(t => t.Id == id).SingleOrDefault<Test>();
            context.Test.Remove(Del);
            context.SaveChanges();

            return RedirectToAction("AddStudent");
        }

        public IActionResult Edit(int? id)
        {
            Test test = context.Test.Where(t => t.Id == id).SingleOrDefault<Test>();
            return View(test);

        }
        [HttpPost]
        public IActionResult Edit([Bind(include: "Id,Name,Password,Profile,Cv")] Test test)
        {
            if(ModelState.IsValid)
                context.Entry(test).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("AddStudent");
            }
        [HttpGet]
        public IActionResult Login()
        {
           
            return View();

        }
        [HttpPost]
        public IActionResult Login( Test test)
        {
            //Test SearcExistence = context.Test.Where(e => e.Name == test.Name).SingleOrDefault<Test>();
            //if (SearcExistence != null)
            //{
            //    ViewBag.searchmessage = "User name already exist you chose  ";
            //    for( int i=2; i<3;)
            //    {
            //        string name = ".ahmad";
            //        string name2 = ".ajmal";

            //        ViewBag.ret = string.Concat(SearcExistence.Name, i++ ,i++);
            //        ViewBag.ret1 = string.Concat(SearcExistence.Name, name);
            //        ViewBag.ret2 = string.Concat(SearcExistence.Name, name2, i++);
            //    }
            //}
            //else
            //{
            //    context.Test.Add(test);
            //    context.SaveChanges();
            //    ViewBag.success = "enterd sexfully";
            //}
            if (ModelState.IsValid)
            {
                Test login = context.Test.Where(e => e.Name == test.Name && e.Password == test.Password).SingleOrDefault<Test>();
                return View(login);
            }
            else
            {
                return View();
            }
           
            
        }

        public IActionResult SearchStudent()
        {
            return View();
        }


        public string SearchStudentAjax(int Search)
        {
            Test t = context.Test.FirstOrDefault<Test>(e=>e.Id==Search);
            string data = "";





            data += "<table class='table table-striped'>";
            data += "<tr>";

            data += "<td>";
            data += t.Id;
            data += "</td>";

            data += "<td>";
            data += t.Name;
            data += "</td>";

            data += "<td>";
            data += t.Password;
            data += "</td>";


            data += "<td>";
            data += "<img src='/data/pp/ " + t.Profile+ "' />";
            data += "</td>";


          

            data += "</tr>";
            data += "</table>";

            //data += "<div class='col-md- 12 background1'>";
            //data += "<div class='col-md- 3 '>";
            //data += "this is the first div designed by dynamically";
            //data +="</div>";
            //data += "<div class='col-md- 3 '>";
            //data += "this is the first div designed by dynamically";
            //data += "</div>";
            //data += "<div class='col-md- 3 '>";
            //data += "this is the first div designed by dynamically";
            //data += "</div>";
            //data += "</div>";
            return data;

        }


     public IActionResult ShowStudent()
        {

            return View();
        }  
     public string SearchStudentAjax1()
        {
            string data = "Login";
            
            return data;
         
        } 
        
        public IActionResult ListProduct()
        {
            IList<Test> product = context.Test.ToList<Test>();
            return View(product);
        }
        public IActionResult AddCart(int? id)
        {
            IList<Test> cart = context.Test.Where(e=>e.Id==id).ToList<Test>();
            return View(cart);
        }
         class student
        {
            public string from { get; set; }
            public string to { get; set; }
            public string text { get; set; }
        }
        public string Api()
        {
           // json file making and using json file

            student s = new student();
            s.from = "info sms";
            s.to = "54736357574";
            s.text = "khdkhlsjlsdjl jljdl";
            string data = JsonConvert.SerializeObject(s);
            string abc = "{'from':'InfoSMS','to':'41793026727', 'text':'Test SMS.'}";
            student s2 = JsonConvert.DeserializeObject<student>(abc);

            return data;


        }
        public IActionResult ProductShow()
        {
            IList<Test> dynamic = context.Test.ToList<Test>();
            return View(dynamic);
        }
    }
    }
