using AutoMapper;
using System;
using System.Collections.Generic;
using WebApplication1.ApplicationLayer.Mapper.Infrastructure;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.MovieViewModels;
using WebApplication1.ApplicationLayer.Services.Abstraction;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.ApplicationLayer.Mapper.Repositories
{
    public class MappedMovie : IMappedMovie
    {
        private readonly IMovieRepository _repo;
        private IMapper _mapper;

        public MappedMovie(IMovieRepository repo, IMapper mapper)
        {
            this._repo = repo;
            this._mapper = mapper;
        }

        public void AddMappedMovie(CreateMovieVm vm, Guid[] locations)
        {
            Movie movie = _mapper.Map<Movie>(vm);
            _repo.Add(movie, locations);
        }

        public IEnumerable<MovieVm> GetAllMappedMovies()
        {
            var movies = _repo.GetMovies();
            var vm = _mapper.Map<List<MovieVm>>(movies);
            return vm;
        }

       

        public EditMovieVm GetMappedMovie(Guid id)
        {
            Movie movie = _repo.GetMovie(id);

            EditMovieVm vm = _mapper.Map<EditMovieVm>(movie);

            return vm;
        }

        public void UpdateMappedMovie(EditMovieVm vm, Guid[] locations)
        {
            var movie = _mapper.Map<Movie>(vm);
            _repo.Update(movie,locations);
        }


        public DeleteMovieVm GetDeleteMappedMovie(Guid id)
        {
            var movie = _repo.GetMovie(id);
            //var movie = _repo.GetMovie(id);
            return _mapper.Map<DeleteMovieVm>(movie);
        }



        public void DeleteMappedMovie(DeleteMovieVm vm)
        {


            //Movie movie = _mapper.Map<Movie>(vm);

            var movie = _mapper.Map(vm, _repo.GetMovie(vm.Id));
            _repo.Delete(movie);
            
            
        
            
        }

        

        

        public DetailMovieVm GetMappedDetails(Guid id)
        {
            //Movie movie = _repo.GetMovie(id);
            var movie = _repo.GetMovie(id);
            return _mapper.Map<DetailMovieVm>(movie);

           
            //var vm = _mapper.Map<DetailMovieVm>(movie);
            //return vm;
        }

        public void Save()
        {
            _repo.Save();
        }


       
    }
}
