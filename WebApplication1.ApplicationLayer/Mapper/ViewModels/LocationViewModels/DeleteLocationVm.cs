using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.ApplicationLayer.Mapper.ViewModels.LocationViewModels
{
    public class DeleteLocationVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Room { get; set; }
        public int Capacity { get; set; }
        public Guid[] Movies { get; set; }
    }
}
