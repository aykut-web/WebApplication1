using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.ApplicationLayer.Services.Abstraction;
using WebApplication1.DomainLayer.Entities.Concrete;
using WebApplication1.InfrastructureLayer.Repositories.Kernel;

namespace WebApplication1.ApplicationLayer.Services.Concrete
{
    public class MovieService : IMovieRepository
    {
        private readonly IRepositories<Movie> _repo;

        public MovieService(IRepositories<Movie> repo)
        {
            this._repo = repo;
        }

        public void Add(Movie movie, Guid[] locations)
        {
            foreach (Guid id in locations)
            {
                movie.Locations.Add(new LocationMovies { Movie = movie, LocationId = id });
            }
            _repo.Add(movie);
        }

        public void Delete(Movie movie)
        {

            _repo.Delete(movie);
        }

        public Movie GetMovie(Guid id)
        {
            return _repo.GetById(id);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return _repo.GetAll().AsQueryable().Include(l => l.Locations).ThenInclude(l => l.Location);
        }

        public IEnumerable<Location> GetLocationMovies(Guid id)
        {
            var movie = _repo.GetById(id);
            var locationMovies = movie.Locations.Select(x => x.Location);
            return locationMovies;
        }
        public void Save()
        {
            _repo.Save();
        }

        public void Update(Movie movie, Guid[] locations)
        {
            movie.Locations = new HashSet<LocationMovies>();

            foreach (Guid id in locations)
            {
                movie.Locations.Add(new LocationMovies { LocationId = id });
            }
            _repo.Update(movie);
        }
    }
}
