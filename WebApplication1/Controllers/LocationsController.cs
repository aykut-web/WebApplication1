using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.ApplicationLayer.Mapper.Infrastructure;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.LocationViewModels;
using WebApplication1.DomainLayer.Entities.Concrete;
using WebApplication1.InfrastructureLayer.Context;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        private readonly IMappedLocation _locationRepo;
        private readonly IMappedMovie _movieRepo;

        public LocationsController(IMappedLocation locationRepo, IMappedMovie movieRepo)
        {
            this._locationRepo = locationRepo;
            this._movieRepo = movieRepo;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var location = _locationRepo.GetAllMappedLocations();
            return View(location);
        }

        public IActionResult Create()
        {
            ViewBag.Movies = new SelectList(_movieRepo.GetAllMappedMovies(), "Id", "Name");
            return View();
            
        }

        [HttpPost]
        public IActionResult Create(CreateLocationVm vm, Guid[] movies)
        {
            if (ModelState.IsValid)
            {
                _locationRepo.AddMappedLocation(vm, movies);
                _locationRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Movies = new SelectList(_movieRepo.GetAllMappedMovies(), "Id", "Name");
            return View(vm);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var location = _locationRepo.GetAllMappedLocations()
                .Where(p => p.Id == id.Value)
                .AsQueryable()
                .Include(m => m.Movies).Select(x => new EditLocationVm
                {
                    Id = x.Id,
                    Room = x.Room,
                    Capacity = x.Capacity,
                    Movies = x.Movies
                }).FirstOrDefault();

            if (location == null)
            {
                return NotFound();
            }
            ViewBag.Movies = new SelectList(_movieRepo.GetAllMappedMovies(), "Id", "Name");
            return View(location);

        }

        [HttpPost]
        public IActionResult Edit(EditLocationVm vm, Guid[] movies)
        {
            if (ModelState.IsValid)
            {
                _locationRepo.UpdateMappedLocation(vm, movies);
                _locationRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Movies = new SelectList(_movieRepo.GetAllMappedMovies(), "Id", "Name");
            return View(vm);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var location = _locationRepo.GetDeleteMappedLocation(id.Value);
            if (location==null)
            {
                return NotFound();
            }
            return View(location);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var location = _locationRepo.GetDeleteMappedLocation(id);
            _locationRepo.DeleteMappedLocation(location);
            _locationRepo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(Guid? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var location=_locationRepo.GetMappedDetails(id.Value);
            if (location==null)
            {
                return NotFound();
            }
            return View(location);
        }


        public JsonResult _GetLocationsReport()
        {
            var moviesCount = (_locationRepo.GetAllMappedLocations().GroupBy(c=> new {c.Name, c.Movies }).Select(c => new
            {
                labels = c.Key.Name,
                data = c.Key.Movies.Count()
            }));





            //_context.Movies.GroupBy(c => new { c.Location.Id, c.Location.Name })
            //    .Select(c => new
            //    {
            //        labels = c.Key.Name,
            //        data = c.Count()
            //    }));

            return Json(new
            {
                labels = moviesCount.Select(x => x.labels).ToArray(),
                data = moviesCount.Select(x => x.data).ToArray()
            });
        }









        //public JsonResult _GetLocationsReport()
        //{
        //    var moviesCount= _movieRepo.GetAllMappedMovies().





        //    //var moviesCount = (_context.Movies.GroupBy(c => new { c.Location.Id, c.Location.Name })
        //    //    .Select(c => new
        //    //    {
        //    //        labels = c.Key.Name,
        //    //        data = c.Count()
        //    //    }));

        //    //return Json(new
        //    //{
        //    //    labels = moviesCount.Select(x => x.labels).ToArray(),
        //    //    data = moviesCount.Select(x => x.data).ToArray()
        //    //});
        //}










        // GET: Locations
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Locations.ToListAsync());
        //}

        //// GET: Locations/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var location = await _context.Locations
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(location);
        //}

        //// GET: Locations/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Locations/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Room,Capacity,Id,Name")] Location location)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        location.Id = Guid.NewGuid();
        //        _context.Add(location);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(location);
        //}

        //// GET: Locations/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var location = await _context.Locations.FindAsync(id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(location);
        //}

        //// POST: Locations/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Room,Capacity,Id,Name")] Location location)
        //{
        //    if (id != location.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(location);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LocationExists(location.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(location);
        //}

        //// GET: Locations/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var location = await _context.Locations
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (location == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(location);
        //}

        //// POST: Locations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var location = await _context.Locations.FindAsync(id);
        //    _context.Locations.Remove(location);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LocationExists(Guid id)
        //{
        //    return _context.Locations.Any(e => e.Id == id);
        //}


        // bir lokasyonda kaç tane film var 


    }
}
