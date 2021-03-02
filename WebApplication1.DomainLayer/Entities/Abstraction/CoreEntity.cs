using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DomainLayer.Entities.Abstraction
{
    public class CoreEntity
    {

        public CoreEntity()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
