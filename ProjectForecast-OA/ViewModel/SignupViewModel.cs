using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Name is needed！")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is needed！")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        //[Required(ErrorMessage = "必须输入密码！")]
        //[DataType(DataType.Password)]
        //[Display(Name = "密码")]
        //public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Country is needed！")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Role is needed！")]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}