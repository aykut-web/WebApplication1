using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DomainLayer.Entities.Abstraction;

namespace WebApplication1.DomainLayer.Entities.Concrete
{
    public class Location: CoreEntity
    {
        public Location()
        {
            this.Movies = new HashSet<LocationMovies>();
            //this.Employees = new HashSet<Employee>();
        }
        public virtual ICollection<LocationMovies> Movies { get; set; }
        //public virtual ICollection<Employee> Employees { get; set; }




        public int Room { get; set; }
        public int Capacity { get; set; }



    }


}
