using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Vidly3.Models;
using Vidly3.ViewModels;

namespace Vidly3.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DataContext _context;

        public MoviesController()
        {
            _context = new DataContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();
            //return View(movies);
            return View();
        }

        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                Movie movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            MovieFormViewModel viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

    }


}