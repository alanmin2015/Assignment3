using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string SearchKey, string SearchKey2, string SearchKey3)
        {
            //Debug.WriteLine("The user is trying to for"+SearchKey);

            TeacherDataController controller = new TeacherDataController();
          
          
                IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey, SearchKey2,SearchKey3);
           
       

            return View(Teachers);
        }

       


        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //Get: /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        // POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string Employeenumber, string Hiredate, string Salary)
        {
            TeacherDataController MyController = new TeacherDataController();

            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.Employeenumber = Employeenumber;
            NewTeacher.Hiredate = Hiredate;
            NewTeacher.Salary = Salary;

            MyController.AddTeacher(NewTeacher);
            
            
            Debug.WriteLine("Trying to create a teacher with title " + TeacherFname);
            
            return RedirectToAction("List");
        }

        //GET: /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //GET: /Teacher/Edit/{id}
        [HttpGet]

        public ActionResult Update(int id)
        {
            TeacherDataController MyController = new TeacherDataController();
            Teacher SelectedTeacher= MyController.FindTeacher(id);
            //Views/Teacher/Edit.cshtml
            return View(SelectedTeacher);
        }

        [HttpGet]

        public ActionResult Validation (int id)
        {
            TeacherDataController MyController = new TeacherDataController();
            Teacher Validation = MyController.FindTeacher(id);
            //Views/Teacher/Edit.cshtml
            return View(Validation);
        }

        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string Employeenumber, string Hiredate, string Salary)
        {
            Debug.WriteLine("Trying to update an teacher");
            Debug.WriteLine("id");
            Debug.WriteLine("TeacherFname");
            Teacher UpdatedTeacher=new Teacher();

            TeacherDataController MyController = new TeacherDataController();
            Teacher Validation = MyController.FindTeacher(id);

            UpdatedTeacher.TeacherFname = TeacherFname;
            UpdatedTeacher.TeacherLname = TeacherLname;
            UpdatedTeacher.Employeenumber = Employeenumber;
            UpdatedTeacher.Hiredate = Hiredate;
            UpdatedTeacher.Salary = Salary;
            // server side validation
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(Employeenumber) ||  string.IsNullOrEmpty(Hiredate) || string.IsNullOrEmpty(Salary))
            {
                Debug.WriteLine("Validation Failed");
                return View("Validation");
            }
      
            MyController.UpdateTeacher(id, UpdatedTeacher);
            return RedirectToAction("list");
        }
    }
}