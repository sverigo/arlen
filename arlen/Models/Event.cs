﻿using System;
using System.ComponentModel.DataAnnotations;

namespace arlen.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Title не заполнено!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длинна поля Title должна быть в диапазоне от 3 до 30 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле Content не заполнено!")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Поле Date не заполнено!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Поле Place не заполнено!")]
        public string Place { get; set; }

        public string Images { get; set; }

        public string Files { get; set; }

        public DateTime CreateTime { get; set; }
    }
}