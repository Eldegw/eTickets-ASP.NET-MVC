using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorServices _services;

        public ActorsController(IActorServices services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _services.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCreate(Actor actor)
        {
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
               await _services.AddAsync(actor);
               return RedirectToAction("Index");
            }
            return View("Create" , actor);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _services.GetByIdAsync(id);
            if (actor == null)
            {
                return View("NotFound");
            }
            return View("Delete",actor);
        }

        [HttpPost ]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          await _services.DeleteAsync(id);
          return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
           var actorDetails =await _services.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
          
            return View("Details", actorDetails);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id )
        {
            var editId = await _services.GetByIdAsync(id);
            if (editId == null)
            {
                return View("NotFound");
            }
            return View("Edit",editId);

        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(int id , Actor actor )
        {
            if (ModelState.IsValid)
            {

               await _services.UpdateAsync(id, actor);
                return RedirectToAction("Index");

            }

            return View("Edit");
           

        }

    }
}
