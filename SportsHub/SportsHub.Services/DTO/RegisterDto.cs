﻿using System.ComponentModel.DataAnnotations;

namespace SportsHub.Services.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Поле електронна пошта є обов'язковим")]
        [EmailAddress(ErrorMessage = "Введене поле не є правильним для електронної пошти")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле пароль є обов'язковим")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$",
            ErrorMessage = "Пароль повинен містити літери, цифри та знаки")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле повторення пароля є обов'язковим")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле ім'я є обов'язковим")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ']{1,20}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ']{1,20})*$",
            ErrorMessage = "Ім'я має містити тільки літери")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле прізвище є обов'язковим")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯІіЄєЇїҐґ']{1,20}((\s+|-)[a-zA-Zа-яА-ЯІіЄєЇїҐґ']{1,20})*$", ErrorMessage = "Прізвище має містити тільки літери")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Поле місце є обов'язковим")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Поле вид спорту є обов'язковим")]
        public string SportKindId { get; set; }
    }
}
