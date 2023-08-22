using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class AdminProcessor
    {
        

        public static int CreateAdmin(int adminID, string uniName, string firstName, string lastName, string emailAddress, string password)
        {
            // Check if the email address already exists in the database
            bool isDuplicate = SqlDataAccess.CheckForDuplicateEmailAddress(emailAddress);

            if (isDuplicate)
            {
                // You can handle the duplicate case here
                // For example, you can throw an exception, return an error code, or handle it in any other way suitable for your application's logic.
                // In this example, let's throw an exception to indicate the duplicate case.
                //throw new Exception("Duplicate email address found.");
                return 0;
            }

            else
            {
                AdminModel data = new AdminModel
                {
                    Id = adminID,
                    UniversityName = uniName,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    Password = password

                };


                string sql = @"insert into dbo.Admin (UniversityName, FirstName, LastName, EmailAddress, Password) values (@UniversityName, @FirstName, @LastName, @EmailAddress, @Password);";
                return SqlDataAccess.SaveData(sql, data);
            }
        }

        public static List<AdminModel> LoadAdmin()
        {
            string sql = "select UniversityName, FirstName, LastName, EmailAddress from dbo.Admin;";
            return SqlDataAccess.LoadData<AdminModel>(sql);
        }

        public static AdminModel LoginAdmin(string emailAddress, string password)
        {

            var validLogin = SqlDataAccess.LoginUsingEmail(emailAddress, password);
            if (validLogin != null)
            {
                return validLogin;
            }

            else
            {
                return null;
            }

        }

        public static int CreateStudent(int studentId, int adminID, string degreeName, string address, int admissionYear, double admissionFee,  string firstName, string lastName, string emailAddress, string password)
        {
            // Check if the email address already exists in the database
            bool isDuplicate = SqlDataAccess.CheckForDuplicateEmailAddress(emailAddress);

            if (isDuplicate)
            {
                // You can handle the duplicate case here
                // For example, you can throw an exception, return an error code, or handle it in any other way suitable for your application's logic.
                // In this example, let's throw an exception to indicate the duplicate case.
                //throw new Exception("Duplicate email address found.");
                return 0;
            }

            else
            {
                string sql = "insert into dbo.Student (AdminID, FirstName, LastName, Address, DegreeName, AdmissionYear, AdmissionFee, StudentEmail, Password) values (@AdminId,  @FirstName, @LastName, @Address, @DegreeName, @AdmissionYear, @AdmissionFee, @StudentEmail, @Password);";
                //StudentModel data = new StudentModel
                //{
                //    Id = studentId,
                //    AdminId = adminID,
                //    DegreeName = degreeName,
                //    Address = address,
                //    AdmissionYear = admissionYear,
                //    AdmissionFee = admissionFee,
                //    FirstName = firstName,
                //    LastName = lastName,
                //    StudentEmailAddress = emailAddress,
                //    Password = password

                //};
                return SqlDataAccess.SaveData(sql, new {
                    Id = studentId,
                    AdminId = adminID,
                    DegreeName = degreeName,
                    Address = address,
                    AdmissionYear = admissionYear,
                    AdmissionFee = admissionFee,
                    FirstName = firstName,
                    LastName = lastName,
                    StudentEmail = emailAddress,
                    Password = password
                });
            }
        }

        public static AdminModel GetUserById(int userID)
        {
            var currentUser = SqlDataAccess.GetUserById(userID);
            if (currentUser != null)
            {
                return currentUser;
            }

            else
            {
                return null;
            }
        }

        public static IEnumerable<StudentModel> GetAllStudentsById(int adminId)
        {
            var currentUser = SqlDataAccess.GetAllStudentsById(adminId);
            if (currentUser != null)
            {
                return currentUser;
            }

            else
            {
                return null;
            }
        }

        public static StudentModel GetStudentInformation(int id)
        {
            var currentUser = SqlDataAccess.GeStudentById(id);
            if (currentUser != null)
            {
                return currentUser;
            }

            else
            {
                return null;
            }
        }

        public static void UpdateStudentInformation(StudentModel updatedStudent)
        {
            // Use your data access logic to update the student's information in the database
            SqlDataAccess.UpdateStudentInformation(updatedStudent);
        }

        public static void DeleteStudentInformation(int id)
        {
            SqlDataAccess.DeleteStudentInformation(id);
        }

        public static IEnumerable<StudentModel> SearchStudents(object searchinput)
        {
            var currentUser = SqlDataAccess.SearchStudents(searchinput);
            if (currentUser != null)
            {
                return currentUser;
            }

            else
            {
                return null;
            }
        }
    }
}