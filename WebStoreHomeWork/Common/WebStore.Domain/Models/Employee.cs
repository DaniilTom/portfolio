using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Employee
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [RegularExpression(@"(^[A-Z][a-z]{3,100}$|^[А-Я][а-я]{3,100}$)")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z][a-z]{3,100}$|^[А-Я][а-я]{3,100}$")]
        [Required(ErrorMessage = "Фамилия обязательна")]
        public string SurName { get; set; }

        public string Patronymic { get; set; }

        [Required, Range(20, 30, ErrorMessage = "Возраст д.б. от 20 до 30")]
        public int Age { get; set; }


        public string Dossier { get; set; }
    }
}
