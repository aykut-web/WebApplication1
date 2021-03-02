using System;
using System.Collections.Generic;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.MovieViewModels;

namespace WebApplication1.ApplicationLayer.Mapper.Infrastructure
{
    public interface IMappedMovie
    {
        IEnumerable<MovieVm> GetAllMappedMovies();
        void AddMappedMovie(CreateMovieVm vm, Guid[] locations);
        void UpdateMappedMovie(EditMovieVm vm, Guid[] locations);
        EditMovieVm GetMappedMovie(Guid id);
        void DeleteMappedMovie(DeleteMovieVm vm);
        DeleteMovieVm GetDeleteMappedMovie(Guid id);
        DetailMovieVm GetMappedDetails(Guid id);
        void Save();

        

    }
}
