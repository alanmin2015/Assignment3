﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Student/List
        public ActionResult List(string SearchKey)
        {
        

            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);


            return View(Students);
        }

        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);


            return View(NewStudent);
        }


    }
}