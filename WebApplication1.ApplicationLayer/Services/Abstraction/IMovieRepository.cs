using System;
using System.Collections.Generic;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.ApplicationLayer.Services.Abstraction
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        IEnumerable<Location> GetLocationMovies(Guid id);
        Movie GetMovie(Guid id);
        void Add(Movie movie, Guid[] locations);
        void Update(Movie movie, Guid[] locations);
        void Delete(Movie movie);
        void Save();
    }
}
