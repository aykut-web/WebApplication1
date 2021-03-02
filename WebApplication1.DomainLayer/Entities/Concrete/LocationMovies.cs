using System;

namespace WebApplication1.DomainLayer.Entities.Concrete
{
    public class LocationMovies
    {
        public Guid MovieId { get; set; }
        public Guid LocationId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Location Location { get; set; }
    }
}
