using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace University_Student_Management_System.Models
{
    public class UniAdmin
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "University Name")]
        //[Required(ErrorMessage = "Please enter your university name.")]
        public string UniversityName { get; set; }

        [Display(Name = "First Name")]
        //[Required(ErrorMessage = "You need to give us your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        //[Required(ErrorMessage = "You need to give us your last name.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "You need to give us your email address.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Confirm Email")]
        //[Compare("EmailAddress", ErrorMessage = "The Email and Confirm Email must match.")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You must have a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "You need to provide a long enough password.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Your password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}