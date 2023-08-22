using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DataLibrary.Models
{
    public class StudentModel
    {
        public int Id { get; set; }

        public int AdminId { get; set; }
        public string DegreeName { get; set; }
        public string Address { get; set; }
        public int AdmissionYear { get; set; }
        public double AdmissionFee { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentEmailAddress { get; set; }
        public string Password { get; set; }
    }
}
