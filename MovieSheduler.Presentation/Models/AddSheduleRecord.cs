using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MovieSheduler.Presentation.Core.ValidationAttributes;

namespace MovieSheduler.Presentation.Models
{
    public class AddSheduleRecord
    {
        [Display(Name = "Фильм")]
        [Required(ErrorMessage = "Нужно выбрать фильм")]
        public int MovieId { get; set; }

        [Display(Name = "Кинотеатр")]
        [Required(ErrorMessage = "Нужно выбрать кинотеатр")]
        public int CinemaId { get; set; }

        [Display(Name = "Дата")]
        [Required(ErrorMessage = "Нужно указать дату")]
        public DateTime Date { get; set; }

        [Display(Name = "Сеансы")]
        //[RegexItem("^(?:[01]?[0-9]|2[0-3]):[0-5][0-9]$",ErrorMessage = "Время имет не правильный формат")]
        [RequiredItem(ErrorMessage = "Должен быть хотя бы один сеанс")]
        public List<TimeSpan> TimeList { get; set; } = new List<TimeSpan>();
    }
}