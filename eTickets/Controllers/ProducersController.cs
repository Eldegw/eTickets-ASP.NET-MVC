using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }

        public  async Task<IActionResult> Index()
        {
            var allProducer = await _service.GetAllAsync();
            return View(allProducer);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View("Create");
        }


        [HttpPost]
        public async Task<IActionResult> SaveCreate(Producer producer)
        {
            ModelState.Remove("Id");
          

            if (ModelState.IsValid)
            {
                await _service.AddAsync(producer);
                return RedirectToAction("Index");

            }
            return View("Create" , producer);

          
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return View("NotFound");
            }
            return View("Delete",result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return View("NotFound");

            }

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Details(int id)
        {
          var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
            {
                return View("NotFound");
            }
            return View("Details" , producerDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var editProducer = await _service.GetByIdAsync(id);
            if (editProducer == null)
            {
                return View("NotFound");
            }
            return View("Edit" , editProducer);

        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(int id , Producer producer)
        {
            if (ModelState.IsValid)
            {
              await _service.UpdateAsync(id, producer);
                return RedirectToAction("Index");
            }
            return View("Edit");


        }

    }
}
