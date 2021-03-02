using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.DomainLayer.Entities.Abstraction;

namespace WebApplication1.DomainLayer.Entities.Concrete
{
    public class Employee : CoreEntity
    {
        //public Guid LocationId { get; set; }
        //public Location Location { get; set; }
        public string Title { get; set; }
        public int Tckn { get; set; }
        public int PhoneNumber { get; set; }
        public string ImageFolder { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }


}
