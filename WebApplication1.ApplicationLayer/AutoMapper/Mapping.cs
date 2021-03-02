using AutoMapper;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.LocationViewModels;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.MovieViewModels;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.ApplicationLayer.AutoMapper
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Movie, MovieVm>();
            CreateMap<CreateMovieVm, Movie>();
            CreateMap<Movie, EditMovieVm>();
            CreateMap<Movie, EditMovieVm>().ReverseMap();
            CreateMap<Movie, DeleteMovieVm>();
            //CreateMap<Movie, DeleteMovieVm>().ReverseMap();
            CreateMap(typeof(DeleteMovieVm), typeof(Movie));
            CreateMap<Movie, DetailMovieVm>();


            CreateMap<Location, LocationVm>();
            CreateMap<CreateLocationVm, Location>();
            CreateMap<CreateLocationVm, Location>().ReverseMap();
            CreateMap<Location, EditLocationVm>();
            CreateMap(typeof(EditLocationVm), typeof(Location));
            CreateMap<Location, DeleteLocationVm>();
            CreateMap(typeof(DeleteLocationVm), typeof(Location));
            CreateMap<Location, DetailLocationVm>();
        }
    }
}
