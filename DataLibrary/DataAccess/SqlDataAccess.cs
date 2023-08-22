using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "UniDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }

        public static bool CheckForDuplicateEmailAddress(string emailAddress)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = "select count(*) from dbo.Admin where EmailAddress = @EmailAddress;";
                int count = cnn.ExecuteScalar<int>(sql, new { EmailAddress = emailAddress });

                return count > 0;
            }
        }

        public static AdminModel LoginUsingEmail(string emailAddress, string password)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT COUNT(*) FROM dbo.Admin WHERE EmailAddress = @EmailAddress and Password = @Password;";
                string sql2 = "SELECT * FROM dbo.Admin WHERE EmailAddress = @emailAddress and Password = @password;";
                AdminModel model = new AdminModel();
                model  = cnn.Query<AdminModel>(sql2, new { EmailAddress = emailAddress, Password = password }).FirstOrDefault();
                int count = cnn.ExecuteScalar<int>(sql, new { EmailAddress = emailAddress, Password = password });

                return model;
            }
        }

        public static AdminModel GetUserById(int userId)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //string sql = "SELECT COUNT(*) FROM dbo.Admin WHERE EmailAddress = @EmailAddress and Password = @Password;";
                string sql2 = "SELECT * FROM dbo.Admin WHERE Id = @Id;";
                AdminModel model = new AdminModel();
                model = cnn.Query<AdminModel>(sql2, new { Id = userId }).FirstOrDefault();
                //int count = cnn.ExecuteScalar<int>(sql, new { EmailAddress = emailAddress, Password = password });

                return model;
            }
        }

        public static IEnumerable<StudentModel> GetAllStudentsById(int adminId)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //string sql = "SELECT COUNT(*) FROM dbo.Admin WHERE EmailAddress = @EmailAddress and Password = @Password;";
                string sql2 = "SELECT * FROM dbo.Student WHERE AdminID = @AdminID;";
                //List<StudentModel> model = new List<StudentModel>();
                var model = cnn.Query<StudentModel>(sql2, new { AdminId = adminId }).ToList();
                //int count = cnn.ExecuteScalar<int>(sql, new { EmailAddress = emailAddress, Password = password });
                return model;
            }
        }

        //Will save one model using the "sql" variable which is a statement
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //Returns the number of records affected
                return cnn.Execute(sql, data);
            }
        }

        public static StudentModel GeStudentById(int id)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //string sql = "SELECT COUNT(*) FROM dbo.Admin WHERE EmailAddress = @EmailAddress and Password = @Password;";
                string sql2 = "SELECT * FROM dbo.Student WHERE Id = @Id;";
                StudentModel model = new StudentModel();
                model = cnn.Query<StudentModel>(sql2, new { Id = id }).FirstOrDefault();
                //int count = cnn.ExecuteScalar<int>(sql, new { EmailAddress = emailAddress, Password = password });

                return model;
            }
        }

        public static void UpdateStudentInformation(StudentModel updatedStudent)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = "UPDATE dbo.Student SET DegreeName = @DegreeName, Address = @Address, AdmissionYear = @AdmissionYear, AdmissionFee = @AdmissionFee, FirstName = @FirstName, LastName = @LastName WHERE Id = @Id;";

                cnn.Execute(sql, new { DegreeName = updatedStudent.DegreeName, Address = updatedStudent.Address, AdmissionYear = updatedStudent.AdmissionYear, AdmissionFee = updatedStudent.AdmissionFee, FirstName = updatedStudent.FirstName, LastName = updatedStudent.LastName, Id = updatedStudent.Id });
            }
        }

        internal static void DeleteStudentInformation(int id)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                string sql = "DELETE dbo.Student WHERE Id = @Id;";

                cnn.Execute(sql, new { Id = id });
            }
        }

        public static IEnumerable<StudentModel> SearchStudents(object searchinput)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                //string sql = "SELECT COUNT(*) FROM dbo.Admin WHERE EmailAddress = @EmailAddress and Password = @Password;";
                string sql2 = "SELECT * FROM dbo.Student WHERE FirstName LIKE '%' + @searchString + '%' OR LastName LIKE '%' + @searchString + '%';";
                //List<StudentModel> model = new List<StudentModel>();
                var model = cnn.Query<StudentModel>(sql2, new { searchString = searchinput }).ToList();
                //int count = cnn.ExecuteScalar<int>(sql, new { EmailAddress = emailAddress, Password = password });
                return model;
            }
        }
    }
}