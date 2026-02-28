using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
       private readonly ICinemaService _service;

        public CinemasController(ICinemaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        public IActionResult Create(Cinema cinema)
        {
            return View("Create",cinema);

        }

        [HttpPost]
        public async Task<IActionResult> SaveCreate(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
               await _service.AddAsync(cinema);
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deletedCinema = await _service.GetByIdAsync(id);
            if (deletedCinema == null)
            {
                return View("NotFound");
            }
            return View("Delete" , deletedCinema);
        }

        public async Task<IActionResult> ConfirmDelete(int id )
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            return View("Delete");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            return View("Details",cinemaDetails);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var editCinema = await _service.GetByIdAsync(id);
            if (editCinema == null)
            {
                return View("NotFound");
            }
            return View("edit",editCinema);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(int id , Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, cinema);
                return RedirectToAction("Index");
            }
            return View("Edit");
        }

    }
}
