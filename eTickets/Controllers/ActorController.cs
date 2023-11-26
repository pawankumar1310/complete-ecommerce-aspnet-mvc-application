using eTickets.Data;
using eTickets.Data.IServices;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eTickets.Controllers
    {
    public class ActorController : Controller
        {
        private readonly IActorService _service;
        public ActorController(IActorService service)
            {
            _service = service;
            }
        public async Task<IActionResult> Index()
            {
            var data = await _service.GetallAsync();
            return View(data);
            }
        //Get:Actor/Create
        public async Task<IActionResult> Create()
            {
            return View();
            }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
            {
            //if(!ModelState.IsValid)
            //{
            //    return View(actor);
            //}
            try
                {
                await _service.AddAsync(actor);
                return RedirectToAction(nameof(Index));
                }
            catch (Exception ex)
                {

                throw ex;
                }

            }
        //Get :Actor/Details/1
        public async Task<IActionResult> Details(int id)
            {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
                return View("Not found");
            return View(actorDetails);
            }
        //Get:Actor/Edit/1
        public async Task<IActionResult> Edit(int id)
            {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
                return View("Not found");
            return View(actorDetails);
            }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
            {
            //if(!ModelState.IsValid)
            //    {
            //    return View(actor);
            //    }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
            }

        //Get : Actor/Delete/1
        public async Task<IActionResult> Delete(int id)
            {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
                return View("Not found");
            return View(actorDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var actorDetails = await _service.GetByIdAsync(id);
            if(actorDetails == null)
                {
                return View("Not found");
                }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
            }
        }
        
    }
