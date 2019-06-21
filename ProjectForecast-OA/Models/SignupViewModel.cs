using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Name is ！")]
        [Display(Name = "账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "必须输入密码！")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required(ErrorMessage = "必须输入密码！")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "必须输入国家！")]
        [Display(Name = "国家")]
        public string Country { get; set; }
    }
}