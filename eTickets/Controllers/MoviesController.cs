using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
      
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n=>n.Cinema);
            var orderedMovies = allMovies
            .OrderBy(m => m.Name);
            return View(orderedMovies);
        }


        public async Task<IActionResult> Details(int id )
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails == null)
            {
                return View("NotFound");
            }
            return View("Details",movieDetails);
        }


        public async Task<IActionResult> Create(MovieViewModel movieViewModel)
        {
            var movieDropdownData = await _service.GetNewMovieDropdownsValue();


            ViewBag.Actors = new SelectList(movieDropdownData.Actors, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers, "Id", "FullName");

            return View("Create",movieViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCreate(MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                await _service.AddNewMovieAsync(movieViewModel);
                return RedirectToAction("Index");

            }
            return View("Create");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
          
            var movieDetails = await _service.GetMovieByIdAsync(id);

         
            if (movieDetails == null) return View("NotFound");

        
            var response = new MovieViewModel()
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
           
                ActorsId = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

       
            var movieDropdownsData = await _service.GetNewMovieDropdownsValue();

          
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

        
            return View("Edit",response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
              await _service.UpdateMovieAsync(movie);
                return RedirectToAction("Index");

            }
            return View("Edit");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var deletedMovie = await _service.GetMovieByIdAsync(id);
            if (deletedMovie == null)
            {
                return View("NotFound");
            }
            return View("Delete",deletedMovie);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
               await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            return View("Delte");
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            // جلب الأفلام مع البيانات المرتبطة
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                // تم تحديث السطر التالي ليشمل البحث في الفئة (Category) أيضاً
                var filteredResult = allMovies.Where(n =>
                    n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                    (n.Description != null && n.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)) ||
                    n.MovieCategory.ToString().Contains(searchString, StringComparison.CurrentCultureIgnoreCase) // سطر البحث في الفئة
                ).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }
    }


}

