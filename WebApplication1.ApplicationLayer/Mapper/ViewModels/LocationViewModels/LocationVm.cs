using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.ApplicationLayer.Mapper.ViewModels.LocationViewModels
{
   public class LocationVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Room { get; set; }
        public int Capacity { get; set; }
        public ICollection<LocationMovies> Movies { get; set; }
    }
}
