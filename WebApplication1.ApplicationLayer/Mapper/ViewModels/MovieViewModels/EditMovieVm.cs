using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.ApplicationLayer.Mapper.ViewModels.MovieViewModels
{
    public class EditMovieVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string ImageFolder { get; set; }
        public int Sales { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public ICollection<LocationMovies> Locations { get; set; }

    }
}
