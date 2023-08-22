using University_Student_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using DataLibrary.BusinessLogic;
using System.Net.Mail;
using System.Net;
using DataLibrary.Models;
using System.Diagnostics;

namespace University_Student_Management_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Admin Register Page";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UniAdmin model)
        {
            
            if (ModelState.IsValid)
            {
                int records = AdminProcessor.CreateAdmin(model.Id, model.UniversityName, model.FirstName, model.LastName, model.EmailAddress, model.Password);
                
                if (records > 0)
                {
                    return RedirectToAction("Index");
                }

                else {
                    TempData["Fail"] = "User already exists please try again!";
                    return View(); 
                }

            }
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Login Page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UniAdmin model)
        {
            if (ModelState.IsValid)
            {
                var user = AdminProcessor.LoginAdmin(model.EmailAddress, model.Password);
                
                if (user != null)
                {
                    StudentStatic.ID = user.Id;
                    return RedirectToAction("UserPage", user);
                }

                else
                {
                    TempData["Fail"] = "User is not in the database please try again!";
                    return View();
                }

            }

            return View();
        }


        public ActionResult UserPage(UniAdmin currentUser)
        {
            ViewBag.Message = "User Page.";
            //StudentStatic.ID = currentUser.Id;
            return View(currentUser);
        }

        public ActionResult AddStudent()
        {
            ViewBag.Message = "Add Student Page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(UniStudent student)
        {
            ViewBag.Message = "Add Student Page.";
            //int userID = 0; // Default value if TempData["UserID"] is not set
            //if (TempData["UserID"] != null)
            //{
            //    userID = (int)TempData["UserID"];
            //}

            if (ModelState.IsValid)
            {
                if(StudentStatic.ID >= 1)
                {
                    var user = AdminProcessor.CreateStudent(student.StudentId, StudentStatic.ID, student.DegreeName, student.Address, student.AdmissionYear, student.AttendanceFee, student.StudentFirstName, student.StudentLastName, student.StudentEmailAddress, student.Password);
                    if (user == 1)
                    {
                        TempData["Success"] = student.StudentFirstName + " is now in the system!";
                        var currentUser = AdminProcessor.GetUserById(StudentStatic.ID);
                        return RedirectToAction("UserPage", currentUser);
                    }

                    else
                    {
                        TempData["Fail"] = "User could not be created please try again!";
                        return View();
                    }
                }

                else { return View(); }
                
            }

            return View();
        }

        public ActionResult EditStudent()
        {
            ViewBag.Message = "Edit Student Information Page.";
            //int userID = 0; // Default value if TempData["UserID"] is not set
            //if (TempData["UserID"] != null)
            //{
            //    userID = (int)TempData["UserID"];
            //}
            //var currentUser = AdminProcessor.GetAllStudentsById((int)TempData["UserID"]);
            return View();
        }

        [HttpPost]
        public ActionResult EditStudent(string searchInput)
        {
            ViewBag.Message = "Edit Student Information Page.";
            if(!string.IsNullOrEmpty(searchInput))
            {
                var searchResults = AdminProcessor.SearchStudents(searchInput);
                return View(searchResults);
            }

            // If search input is empty or null, simply return the view
            return View();
        }

        public ActionResult EditStudentInfo(int id)
        {
            ViewBag.Message = "Edit Student Information Page.";
            //int userID = 0; // Default value if TempData["UserID"] is not set
            //if (TempData["UserID"] != null)
            //{
            //    userID = (int)TempData["UserID"];
            //}

            var currentUser = AdminProcessor.GetStudentInformation(id);
            //return RedirectToAction("EditStudentInfo", currentUser);
            return View(currentUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudentInfo(StudentModel student)
        {
            ViewBag.Message = "Edit Student Information Page.";
            AdminProcessor.UpdateStudentInformation(student);
            var currentUser = AdminProcessor.GetUserById(student.AdminId);
            return RedirectToAction("UserPage", currentUser);
        }


        public ActionResult DeleteStudent(int id)
        {
            ViewBag.Message = "Delete Student Information Page.";

            var currentUser = AdminProcessor.GetStudentInformation(id);
            AdminProcessor.DeleteStudentInformation(id);
            var currentAdmin = AdminProcessor.GetUserById(currentUser.AdminId);
            return RedirectToAction("UserPage", currentAdmin);
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }
}