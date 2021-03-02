using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.LocationViewModels;

namespace WebApplication1.ApplicationLayer.Mapper.Infrastructure
{
    public interface IMappedLocation
    {
        IEnumerable<LocationVm> GetAllMappedLocations();
        void AddMappedLocation(CreateLocationVm vm, Guid[] movies);
        EditLocationVm GetMappedLocation(Guid id);
        void UpdateMappedLocation(EditLocationVm vm, Guid[] movies);
        DeleteLocationVm GetDeleteMappedLocation(Guid id);
        void DeleteMappedLocation(DeleteLocationVm vm);
        DetailLocationVm GetMappedDetails(Guid id);
        void Save();
    }
}
