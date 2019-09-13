using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "Пароль"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
