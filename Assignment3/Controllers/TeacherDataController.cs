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
    public class TeacherDataController : ApiController
    {
        // GET: TeacherDataController

        private BlogDbContext Blog = new BlogDbContext();

        /// <summary>
        /// Returns a list of articles in the systems. If a search key is included we will march the article title to the search key
        /// </summary>


        //This Controller Will access the authors table of our blog database.
        /// <summary>
        /// Returns a list of Teacher in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/teacherdata/listteachers/{SearchKey}/{SearchKey2}/{SearchKey3}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey, string SearchKey2, string SearchKey3)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();
           
            //Open the connection between the web server and database
            Conn.Open();
            string query = "select teachers.*, classes.classname from Teachers LEFT JOIN classes ON teachers.teacherid=classes.teacherid where teacherfname like '%" + SearchKey + "%'";
            //Debug.WriteLine("The search key is " + SearchKey);
           
            if (SearchKey2 != null)
            { query = "select teachers.*, classes.classname from Teachers LEFT JOIN classes ON teachers.teacherid=classes.teacherid where hiredate like '%" + SearchKey2 + "%'";
                Debug.WriteLine("The query is " + query);
            }
            else if(SearchKey3 != null)
                    { query = "select teachers.*, classes.classname from Teachers LEFT JOIN classes ON teachers.teacherid=classes.teacherid where salary like '%" + SearchKey3 + "%'";
                Debug.WriteLine("The query is " + query);
            }
            //string query = "select * from Teachers  where teacherfname like '%" + SearchKey + "%'";
            //Debug.WriteLine("The query is "+query);
            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = query;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string Employeenumber = ResultSet["employeenumber"].ToString();
                string Hiredate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();
                string Course = ResultSet["classname"].ToString();

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.Employeenumber = Employeenumber;
                NewTeacher.Hiredate = Hiredate;
                NewTeacher.Salary = Salary;
                NewTeacher.Course = Course;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        
        /// <summary>
        /// Finds a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>An teacher object</returns>
        [HttpGet]
        [Route("api/teacherdata/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();
         

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string Employeenumber = ResultSet["employeenumber"].ToString();
                string Hiredate = ResultSet["hiredate"].ToString();
                string Salary = ResultSet["salary"].ToString();

                
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.Employeenumber = Employeenumber;
                NewTeacher.Hiredate = Hiredate;
                NewTeacher.Salary = Salary;
            }


            return NewTeacher;
        }
        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
           string query= "INSERT INTO TEACHERS (teacherfname, teacherlname) VALUES (@fname,@lname)";
            MySqlConnection Conn = Blog.AccessDatabase();

       
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@fname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@lname", NewTeacher.TeacherLname);

            Debug.WriteLine("Trying to create a teacher with title " + NewTeacher.TeacherLname);

            cmd.ExecuteNonQuery(); 
            Conn.Close();

        }

        [HttpPost]
        [Route("api/teacherdata/deleteteacher/{TeacherId}")]
        public void DeleteTeacher(int TeacherId)
        {
          
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "delete from teachers where teacherid=@id";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();

        }
    }
}