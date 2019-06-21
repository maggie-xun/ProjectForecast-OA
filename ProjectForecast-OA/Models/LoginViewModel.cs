using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "必须输入账号名！")]
        [Display(Name = "账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "必须输入密码！")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}