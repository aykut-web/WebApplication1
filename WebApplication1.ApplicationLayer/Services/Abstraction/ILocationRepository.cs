using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.ApplicationLayer.Services.Abstraction
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetLocations();
        IEnumerable<Movie> GetLocationMovies(Guid id);
        Location GetLocation(Guid id);
        void Add(Location location, Guid[] movies);
        void Update(Location location, Guid[] movies);
        void Delete(Location location);
        void Save();
    }
}
