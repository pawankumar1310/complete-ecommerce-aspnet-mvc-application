using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
    {
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducerController : Controller
        {
        private readonly IProducerService _service;
        public ProducerController(IProducerService service)
            {
            _service = service;
            }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
            {
            var allProducers = await _service.GetallAsync();
            return View(allProducers);
            }
        //Get:producer/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            {
            var producerDetails = await _service.GetByIdAsync(id);
            if(producerDetails == null)
                {
                return View("Not found");
                }
            return View(producerDetails);
            }
        //Create: producer/create
        public IActionResult Create()
            {
            return View();
            }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
            {
            //if(!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            try
                {
                await _service.AddAsync(producer);
                return RedirectToAction(nameof(Index));
                }
            catch (Exception ex)
                {

                throw ex;
                }

            }

        //Get: producer/Edit/1
        public async Task<IActionResult> Edit(int id)
            {
            var producerDetails = await _service.GetByIdAsync(id);
            if(producerDetails == null)
                {
                return View("Not found");
                }
            return View(producerDetails);
            }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
            {
            //if(!ModelState.IsValid)
            //    {
            //    return View(actor);
            //    }
            if(id == producer.Id)
                {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
                }
            return View(producer);
            }

        //Get : Producer/Delete/1
        public async Task<IActionResult> Delete(int id)
            {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                return View("Not found");
            return View(producerDetails);
            }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
                {
                return View("Not found");
                }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
            }
        }
    }
