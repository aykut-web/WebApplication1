using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebApplication1.ApplicationLayer.Mapper.ViewModels.MovieViewModels
{
    public class DeleteMovieVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string ImageFolder { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public Guid[] Locations { get; set; }


    }
}
