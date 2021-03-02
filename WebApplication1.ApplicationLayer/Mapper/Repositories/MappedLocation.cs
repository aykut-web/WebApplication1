using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.ApplicationLayer.Mapper.Infrastructure;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.LocationViewModels;
using WebApplication1.ApplicationLayer.Services.Abstraction;
using WebApplication1.DomainLayer.Entities.Concrete;

namespace WebApplication1.ApplicationLayer.Mapper.Repositories
{
    public class MappedLocation : IMappedLocation
    {

        private readonly ILocationRepository _repo;
        private IMapper _mapper;

        public MappedLocation(ILocationRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }
        public void AddMappedLocation(CreateLocationVm vm, Guid[] movies)
        {
            Location location = _mapper.Map<Location>(vm);
            _repo.Add(location, movies);
        }

        

        public IEnumerable<LocationVm> GetAllMappedLocations()
        {
            var locations = _repo.GetLocations();
            var vm = _mapper.Map<List<LocationVm>>(locations);
            return vm;

        }

        public EditLocationVm GetMappedLocation(Guid id)
        {
            Location location = _repo.GetLocation(id);
            EditLocationVm vm = _mapper.Map<EditLocationVm>(location);
            return vm;
        }
        public void UpdateMappedLocation(EditLocationVm vm, Guid[] movies)
        {
            var location = _mapper.Map<Location>(vm);
            _repo.Update(location,movies);
        }

        public DeleteLocationVm GetDeleteMappedLocation(Guid id)
        {
            var location = _repo.GetLocation(id);
            return _mapper.Map<DeleteLocationVm>(location);
        }

        public void DeleteMappedLocation(DeleteLocationVm vm)
        {
            var location = _mapper.Map(vm, _repo.GetLocation(vm.Id));
            _repo.Delete(location);
        }

        public DetailLocationVm GetMappedDetails(Guid id)
        {
            var location = _repo.GetLocation(id);
            var vm = _mapper.Map<DetailLocationVm>(location);
            return vm;
        }

        
        public void Save()
        {
            _repo.Save();
        }

        
    }
}
