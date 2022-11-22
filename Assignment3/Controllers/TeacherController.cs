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
        public ActionResult Create(string TeacherFname, string TeacherLname)
        {
            TeacherDataController MyController = new TeacherDataController();

            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;

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
    }
}