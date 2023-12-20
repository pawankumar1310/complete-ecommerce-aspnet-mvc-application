using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
    {
    public class CinemaController : Controller
        {
        private readonly ICinemaService _service;
        public CinemaController(ICinemaService service)
            {
            _service = service;
            }
        public async Task<IActionResult> Index()
            {
            var allCinema = await _service.GetallAsync();
            return View(allCinema);
            }
        //Add:Cinema/Create/1
        public IActionResult Create()
            {
            return View();
            }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
            {
            //if (!ModelState.IsValid) return View(cinema);
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
            }
        //Details : 
        public async Task<IActionResult> Details(int id)
            {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
                return View("Not found");
            return View(cinemaDetails);
            }
        //Get: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
            {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("Not Found");
            return View(cinemaDetails);
            }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
            {
            //if (!ModelState.IsValid) return View(cinema);
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
            }

        //Get: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
            {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("Not Found");
            return View(cinemaDetails);
            }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
            {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("Not Found");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
            }

        }
    }
