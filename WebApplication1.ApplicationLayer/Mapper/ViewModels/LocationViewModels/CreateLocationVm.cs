using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.ApplicationLayer.Mapper.ViewModels.LocationViewModels
{
    public class CreateLocationVm
    {
        public string Name { get; set; }
        public int Room { get; set; }
        public int Capacity { get; set; }
    }
}
