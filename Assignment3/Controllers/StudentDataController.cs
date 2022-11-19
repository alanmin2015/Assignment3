using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using Assignment3.Models;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;



namespace Assignment3.Controllers
{
    public class StudentDataController : ApiController
    {
        // GET: StudentDataController

        private BlogDbContext Blog = new BlogDbContext();


        [HttpGet]
        [Route("api/studentdata/liststudents/{SearchKey}")]
        public IEnumerable<Student> ListStudents(string SearchKey)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            Debug.WriteLine("The search key is " + SearchKey);

            string query = "select * from students where studentfname like '%" + SearchKey + "%'";
            Debug.WriteLine("The query is " + query);
            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string Studentnumber = ResultSet["studentnumber"].ToString();
                string Enroldate = ResultSet["enroldate"].ToString();
          

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.Studentnumber = Studentnumber;
                NewStudent.Enroldate = Enroldate;
            
                Students.Add(NewStudent);
            }

           
            Conn.Close();

     
            return Students;
        }


        [HttpGet]
        [Route("api/studentdata/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL QUERY
            cmd.CommandText = "Select * from Students where studentid = " + id;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index



                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string Studentnumber = ResultSet["studentnumber"].ToString();
                string Enroldate = ResultSet["enroldate"].ToString();


                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.Studentnumber = Studentnumber;
                NewStudent.Enroldate = Enroldate;
            }


            return NewStudent;
        }


    }
}