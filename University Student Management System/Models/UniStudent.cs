using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace University_Student_Management_System.Models
{
    public class UniStudent
    {
        [Key]
        public int StudentId { get; set; }

        [Key]
        public int AdminId { get; set; }

        [Display(Name = "Degree Name")]
        [Required(ErrorMessage = "Please enter the degree name.")]
        public string DegreeName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter the Address.")]
        public string Address { get; set; }

        [Display(Name = "Admission Year")]
        [Required(ErrorMessage = "Please enter the admission year.")]
        public int  AdmissionYear { get; set; }

        [Display(Name = "Admission Fee")]
        [Required(ErrorMessage = "Please enter the admission year.")]
        public double AttendanceFee { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to give us your first name.")]
        public string StudentFirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to give us your last name.")]
        public string StudentLastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Student Email Address")]
        [Required(ErrorMessage = "You need to give us your email address.")]
        public string StudentEmailAddress { get; set; }

        [Display(Name = "Confirm Email")]
        [DataType(DataType.EmailAddress)]
        [Compare("StudentEmailAddress", ErrorMessage = "The Email and Confirm Email must match.")]
        public string ConfirmStudentEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must have a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "You need to provide a long enough password.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}