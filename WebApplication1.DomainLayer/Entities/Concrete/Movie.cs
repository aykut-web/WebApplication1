using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.DomainLayer.Entities.Abstraction;

namespace WebApplication1.DomainLayer.Entities.Concrete
{
    public class Movie: CoreEntity
    {

        public Movie()
        {
            this.Locations = new HashSet<LocationMovies>();
        }
        //public Guid LocationId { get; set; }
        //public Location Location { get; set; }

        public ICollection<LocationMovies> Locations { get; set; }






        [DataType(DataType.Date)]

        [Required]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }


        public string ImageFolder { get; set; } 

        [NotMapped]
        public IFormFile ImageFile { get; set; } 
        [MaxLength(100)]
        public string Description { get; set; }



        //public ICollection<Location> Locations { get; set; }


        //public Movie()
        //{
        //    this.Locations = new HashSet<Location>();
        //}

        //public ICollection<Location> Locations { get; set; }

        public int Sales { get; set; }
    }


}
