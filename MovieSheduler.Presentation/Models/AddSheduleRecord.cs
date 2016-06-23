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
        [RequiredItem(ErrorMessage = "Должен быть хотя бы один сеанс")]
        public List<TimeSpan> SeansList { get; set; } = new List<TimeSpan>();
    }
}