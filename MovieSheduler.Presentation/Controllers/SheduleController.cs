using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MovieSheduler.Application.Cinema;
using MovieSheduler.Application.Cinema.Dtos;
using MovieSheduler.Application.Movie;
using MovieSheduler.Application.Movie.Dtos;
using MovieSheduler.Application.SheduleRecord;
using MovieSheduler.Application.SheduleRecord.Dtos;
using MovieSheduler.Presentation.Models;
using MovieSheduler.Presentation.Core;
using MovieSheduler.Presentation.Core.Messager;
using MovieSheduler.Presentation.Core.ModelBinder;

namespace MovieSheduler.Presentation.Controllers
{
    public class SheduleController : Controller
    {
        private readonly ISheduleRecordService _sheduleRecordService;
        private readonly IMovieService _movieService;
        private readonly ICinemaService _cinemaService;
        private readonly INotifier _notifier;

        public SheduleController(ISheduleRecordService sheduleRecordService, IMovieService movieService, ICinemaService cinemaService,
            INotifier notifier)
        {
            _sheduleRecordService = sheduleRecordService;
            _movieService = movieService;
            _cinemaService = cinemaService;
            _notifier = notifier;
        }

        // GET: Shedule
        public async Task<ActionResult> Index([ModelBinder(typeof(DateModelBinder))] DateTime? date)
        {
            //TODO поставить коммент
            DateTime selectedDate = date ?? await _sheduleRecordService.GetFirstAvailableDate() ?? DateTime.Now;
            GetSheduleByDateOutput shedule = await _sheduleRecordService.GetShedule(selectedDate);
            IReadOnlyCollection<DateTime> availableDates = await _sheduleRecordService.GetAvailableDates();

            var model = new SheduleListViewModel(selectedDate, shedule.SheduleRecords, availableDates);

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
        public async Task<ActionResult> Add(AddSheduleRecord record)
        {
            if (ModelState.IsValid)
            {
                if (!await _sheduleRecordService.RecordExist(new RecordExistInput(record.MovieId, record.CinemaId, record.Date)))
                {
                    return HttpNotFound();
                }

                //коммент
                if (record.TimeList.GroupBy(t => t).Any(tg => tg.Count() > 1))
                {
                    ModelState.AddModelError("time", "Нельзя выбирать несколько одинаковых сеансов");
                }
                if (ModelState.IsValid)
                {
                    var result = _sheduleRecordService.AddRecord(new AddRecordInput(record.MovieId, record.CinemaId, record.TimeList, record.Date));
                    if (result.IsValid)
                    {
                        _notifier.Success("Раписание успешно добавлено!");
                        return RedirectToAction("Index", new { date = record.Date.ToShortDateString() });
                    }
                    ModelState.AddErrorsFromValidationDictionary(result);
                }
                GetAllMoviesOutput movies = await _movieService.GetAllMovies();
                GetAllCinemaOutput cinemas = await _cinemaService.GetAllCinema();

                var model = new AddSheduleRecordViewModel(movies.Movies, cinemas.Cinemas, record);
                return View(model);
            }
            _notifier.Error("При добавление произошла ошибка!");
            return RedirectToAction("Add");
        }

        public async Task<ActionResult> Edit(int cinemaId, int movieId, [ModelBinder(typeof(DateModelBinder))] DateTime date)
        {
            if (!await _sheduleRecordService.RecordExist(new RecordExistInput(movieId, cinemaId, date)))
            {
                return HttpNotFound();
            }

            var model = await GetEditModel(cinemaId, movieId, date);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSheduleRecord editRecord)
        {
            if (ModelState.IsValid)
            {
                if (!await _sheduleRecordService.RecordExist(new RecordExistInput(editRecord.MovieId, editRecord.CinemaId, editRecord.Date)))
                {
                    return HttpNotFound();
                }

                if (editRecord.SeansList.GroupBy(t => t).Any(tg => tg.Count() > 1))
                {
                    ModelState.AddModelError("time", "Нельзя выбирать несколько одинаковых сеансов");
                }

                if (ModelState.IsValid)
                {
                    var result =
                        await _sheduleRecordService.EditRecord(new EditRecordInput(editRecord.CinemaId, editRecord.MovieId, editRecord.Date, editRecord.SeansList));
                    if (result.IsValid)
                    {
                        _notifier.Success("Раписание успешно отредактировано!");
                        return RedirectToAction("Index", new { date = editRecord.Date.ToShortDateString() });
                    }
                    ModelState.AddErrorsFromValidationDictionary(result);
                }

                MovieDto movie = await _movieService.GetMovieById(editRecord.MovieId);
                CinemaDto cinema = await _cinemaService.GetCinemaById(editRecord.CinemaId);
                var model = new EditSheduleRecordViewModel(cinema, movie, editRecord.Date, editRecord.SeansList);
                return View(model);
            }
            _notifier.Error("При редактирование произошла ошибка!");
            return RedirectToAction("Edit", new { editRecord.CinemaId, editRecord.MovieId, date = editRecord.Date.ToShortDateString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int cinemaId, int movieId, [ModelBinder(typeof(DateModelBinder))] DateTime date)
        {
            if (!await _sheduleRecordService.RecordExist(new RecordExistInput(movieId, cinemaId, date)))
            {
                return HttpNotFound();
            }

            var result = await _sheduleRecordService.DeleteRecords(new DeleteRecordsInput(cinemaId, movieId, date));
            if (result.IsValid)
            {
                _notifier.Success("Раписание успешно удалено!");
                return RedirectToAction("Index");
            }

            ModelState.AddErrorsFromValidationDictionary(result);
            var model = await GetEditModel(cinemaId, movieId, date);
            return View("Edit", model);
        }

        private async Task<EditSheduleRecordViewModel> GetEditModel(int cinemaId, int movieId, DateTime date)
        {
            MovieDto movie = await _movieService.GetMovieById(movieId);
            CinemaDto cinema = await _cinemaService.GetCinemaById(cinemaId);
            IReadOnlyCollection<TimeSpan> seansList = await _sheduleRecordService.GetSeansList(new GetSeansonsInput(movieId, cinemaId, date));

            var model = new EditSheduleRecordViewModel(cinema, movie, date, seansList);
            return model;
        }
    }
}