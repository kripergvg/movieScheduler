using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieSheduler.Presentation.Models
{
    public class EditSheduleRecord
    {
        [Required]
        public int CinemaId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Сеансы")]
        public List<TimeSpan> TimeList { get; set; } = new List<TimeSpan>();
    }
}