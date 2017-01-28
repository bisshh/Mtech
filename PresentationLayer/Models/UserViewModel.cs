using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please enter the First Name")]
        [Display(Name = "FirstName")]
        [StringLength(50, ErrorMessage = "Please enter the first name with less than 50 character")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the Middle Name")]
        [Display(Name = "MiddleName")]
        [StringLength(50, ErrorMessage = "Please enter the middle name with less than 50 character")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter the Last Name")]
        [Display(Name = "LastName")]
        [StringLength(50, ErrorMessage = "Please enter the last name with less than 50 character")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter the Email Address")]
        [EmailAddress(ErrorMessage = "Plese enter in valid form, eg. example@example.com")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please provide confirm password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Your Password mismatched")]
        public string ConfirmPassword { get; set; }
        public string Role {get; set;}
    }
}