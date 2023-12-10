using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
    {
    public class MovieController : Controller
        {
        private readonly IMoviesService _service;
        public MovieController(IMoviesService service)
            {
            _service = service;
            }

        public async Task<IActionResult> Index()
            {
            var allMovies = await _service.GetallAsync(n => n.Cinema);
            return View(allMovies);
            }

        public async Task<IActionResult> Filter(string searchString)
            {
            var allMovies = await _service.GetallAsync(n => n.Cinema);
            if(!string.IsNullOrEmpty(searchString))
                {
                var filterResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filterResult);
                }
            return View("Index",allMovies);
            }
        //Get: Movie/Detials/1
        public async Task<IActionResult> Details(int id)
            {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
            }
        //Get : Movie/Create
        public async Task<IActionResult> Create()
            {
            //ViewData["Welcome"] = "Hi user";
            //ViewBag.Description = "This is ViewBag";

            var movieDropdownData = await _service.GetNewMovieDropdownValues();
            ViewBag.Actors = new SelectList(movieDropdownData.actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(movieDropdownData.producers, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(movieDropdownData.cinemas, "Id", "Name");

            return View();
            }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
            {
            //if(!ModelState.IsValid)
            //    {
            //    var movieDropdownData = await _service.GetNewMovieDropdownValues();
            //    ViewBag.Actors = new SelectList(movieDropdownData.actors, "Id", "FullName");
            //    ViewBag.Producers = new SelectList(movieDropdownData.producers, "Id", "FullName");
            //    ViewBag.Cinemas = new SelectList(movieDropdownData.cinemas, "Id", "Name");
            //    return View(movie);
            //    }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
            }

        //Get : Movie/Edit/1
        public async Task<IActionResult> Edit(int id)
            {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if(movieDetails == null)
                {
                return View("Not Found");
                }

            var response = new NewMovieVM()
                {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(a => a.ActorId).ToList()
                };
            //ViewData["Welcome"] = "Hi user";
            //ViewBag.Description = "This is ViewBag";

            var movieDropdownData = await _service.GetNewMovieDropdownValues();
            ViewBag.Actors = new SelectList(movieDropdownData.actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(movieDropdownData.producers, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(movieDropdownData.cinemas, "Id", "Name");

            return View(response);
            }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
            {
            if (id != movie.Id) return View("Not Found");

            //if(!ModelState.IsValid)
            //    {
            //    var movieDropdownData = await _service.GetNewMovieDropdownValues();
            //    ViewBag.Actors = new SelectList(movieDropdownData.actors, "Id", "FullName");
            //    ViewBag.Producers = new SelectList(movieDropdownData.producers, "Id", "FullName");
            //    ViewBag.Cinemas = new SelectList(movieDropdownData.cinemas, "Id", "Name");
            //    return View(movie);
            //    }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
            }

        //Get: Movie/Delete/1
        public async Task<IActionResult> Delete(int id)
            {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null)
                {
                return View("Not Found");
                }
            return View(movieDetails);
            }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if(movieDetails == null)
                {
                return View("Not Found");
                }
             await _service.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
            }
        }
    }
