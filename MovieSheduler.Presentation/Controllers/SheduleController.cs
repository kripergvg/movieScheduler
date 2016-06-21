using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MovieSheduler.Application.Cinema;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Movie;
using MovieSheduler.Application.Movie.Dtos;
using MovieSheduler.Application.SheduleRecord;
using MovieSheduler.Application.SheduleRecord.Dtos;
using MovieSheduler.Presentation.Models;

namespace MovieSheduler.Presentation.Controllers
{
    public class SheduleController : Controller
    {
        private readonly ISheduleRecordService _sheduleRecordService;
        private readonly IMovieService _movieService;
        private readonly ICinemaService _cinemaService;

        public SheduleController(ISheduleRecordService sheduleRecordService, IMovieService movieService, ICinemaService cinemaService)
        {
            _sheduleRecordService = sheduleRecordService;
            _movieService = movieService;
            _cinemaService = cinemaService;
        }

        // GET: Shedule
        public async Task<ActionResult> Index(DateTime? date)
        {
            //TODO поставить коммент
            DateTime selectedDate = date ?? await _sheduleRecordService.GetFirstAvailableDate() ?? DateTime.Now;

            GetSheduleByDateOutput shedule = await _sheduleRecordService.GetShedule(selectedDate);
            var model = new SheduleListViewModel(selectedDate, shedule.SheduleRecords);

            return View(model);
        }

        public async Task<ActionResult> Add()
        {
            GetAllMoviesOutput movies = await _movieService.GetAllMovies();
            GetAllCinemaOutput cinemas = await _cinemaService.GetAllCinema();

            var model = new AddSheduleRecordViewModel(movies.Movies, cinemas.Cinemas);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddSheduleRecord record)
        {
            if (ModelState.IsValid)
            {
                var dtoRecord = record.ToDto();
                var result = _sheduleRecordService.AddRecord(dtoRecord);
                if (result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                //TODO вывод ошибки
                
            }
            return View();

        }
    }
}