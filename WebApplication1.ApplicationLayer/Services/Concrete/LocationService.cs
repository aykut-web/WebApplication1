using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication1.ApplicationLayer.Services.Abstraction;
using WebApplication1.DomainLayer.Entities.Concrete;
using WebApplication1.InfrastructureLayer.Repositories.Kernel;

namespace WebApplication1.ApplicationLayer.Services.Concrete
{
    public class LocationService : ILocationRepository
    {

        private readonly IRepositories<Location> _repo;

        public LocationService(IRepositories<Location> repo)
        {
            this._repo = repo;
        }

        public void Add(Location location, Guid[] movies)
        {
            foreach (Guid id in movies)
            {
                location.Movies.Add(new LocationMovies { Location = location, MovieId = id });
            }
            _repo.Add(location);
        }

        public void Delete(Location location)
        {
            _repo.Delete(location);
        }

        public IEnumerable<Location> GetLocations()
        {
            return _repo.GetAll().AsQueryable().Include(m => m.Movies).ThenInclude(m => m.Movie);
        }

        public Location GetLocation(Guid id)
        {
            return _repo.GetById(id);
        }

        public IEnumerable<Movie> GetLocationMovies(Guid id)
        {
            var location = _repo.GetById(id);
            var locationMovies = location.Movies.Select(x => x.Movie);
            return locationMovies;
        }

        public void Save()
        {
            _repo.Save();
        }

        public void Update(Location location, Guid[] movies)
        {
            location.Movies = new HashSet<LocationMovies>();

            foreach (Guid id in movies)
            {
                location.Movies.Add(new LocationMovies { MovieId = id });
            }
            _repo.Update(location);
        }
    }
}
