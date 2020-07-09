using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly3.Dtos;
using Vidly3.Models;

namespace Vidly3.Controllers.Api
{
    public class MoviesController : ApiController
    {
        public readonly DataContext _context;

        public MoviesController()
        {
            _context = new DataContext();
        }

        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.Include(m => m.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                BadRequest();

            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);

            movie.DateAdded = DateTime.Now;

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Movie movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteMovie(int id)
        {
            Movie movie= _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}
