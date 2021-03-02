using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ApplicationLayer.Mapper.Infrastructure;
using WebApplication1.ApplicationLayer.Mapper.ViewModels.MovieViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        

        private readonly IMappedMovie _repo;
        private readonly IMappedLocation _locationRepo;
        private readonly IWebHostEnvironment _environment;


        public MoviesController(IMappedMovie repo, IWebHostEnvironment environment, IMappedLocation locationRepo)
        {
            this._repo = repo;
            this._environment = environment;
            this._locationRepo = locationRepo;
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            var movies = _repo.GetAllMappedMovies();

            return View(movies);
        }

        public IActionResult Create()
        {
            ViewBag.Locations = new SelectList(_locationRepo.GetAllMappedLocations(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMovieVm vm, Guid[] locations)
        {
            if (ModelState.IsValid)
            {
                var Images = Path.Combine(_environment.WebRootPath, "Image");
                if (vm.ImageFile.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(Images, vm.ImageFile.FileName), FileMode.Create))
                    {
                        vm.ImageFile.CopyTo(fileStream);
                    }
                }
                vm.ImageFolder = vm.ImageFile.FileName;

                _repo.AddMappedMovie(vm, locations);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Locations = new SelectList(_locationRepo.GetAllMappedLocations(), "Id", "Name");
            return View(vm);
        }

        public IActionResult Edit(Guid? id)
        {
            //EditMovieVm vm = _repo.GetMappedMovie(id);

            if (id==null)
            {
                return NotFound();
            }

            var movie = _repo.GetAllMappedMovies().Where(p => p.Id == id.Value)
                .AsQueryable()
                .Include(l => l.Locations).Select(x => new EditMovieVm
                {
                    Name = x.Name,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate,
                    Genre = x.Genre,
                    Locations = x.Locations
                }).FirstOrDefault();

            if (movie==null)
            {
                return NotFound();
            }



            ViewBag.Locations = new SelectList(_locationRepo.GetAllMappedLocations(), "Id", "Name");
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(EditMovieVm vm, Guid[] locations)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateMappedMovie(vm,locations);
                _repo.Save();
                return RedirectToAction(nameof(Index));

            }

            ViewBag.Locations = new SelectList(_locationRepo.GetAllMappedLocations(), "Id", "Name");
            return View(vm);
        }

        public IActionResult Delete (Guid? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var vm = _repo.GetDeleteMappedMovie(id.Value);

            if (vm==null)
            {
                return NotFound();
            }
            return View(vm);
        }
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var vm = _repo.GetDeleteMappedMovie(id);
           
            var folder = @"wwwroot\Image";
            var imagesFile = vm.ImageFolder;
            if (System.IO.File.Exists(Path.Combine(folder, imagesFile)))
            {

                System.IO.File.Delete(Path.Combine(folder, imagesFile));

            }
            
            _repo.DeleteMappedMovie(vm);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var vm = _repo.GetMappedDetails(id.Value);

            if (vm==null)
            {
                return NotFound();
            }
            return View(vm);
        }

























        //public async Task<IActionResult> Index()
        //{

        //    var projectContext = _context.Movies.Include(m => m.Location);
        //    return View(await projectContext.ToListAsync());
        //}

        //public IActionResult Ixndex()
        //{

        //    var movies = _context.GetAllMappedMovie().ToList();

        //    return View(movies);

        //    //var projectContext = _context.Movies.Include(m => m.Location);
        //    //return View(await projectContext.ToListAsync());
        //}

        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies
        //        .Include(m => m.Location)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //public IActionResult Create()
        //{
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LocationId,ReleaseDate,Genre,Sales,ImageFolder,Description,Id,Name")] Movie movie)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        movie.Id = Guid.NewGuid();
        //        _context.Add(movie);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", movie.LocationId);
        //    return View(movie);
        //}

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", movie.LocationId);
        //    return View(movie);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("LocationId,ReleaseDate,Genre,Sales,ImageFolder,Description,Id,Name")] Movie movie)
        //{
        //    if (id != movie.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(movie);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MovieExists(movie.Id))
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
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", movie.LocationId);
        //    return View(movie);
        //}

        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies
        //        .Include(m => m.Location)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MovieExists(Guid id)
        //{
        //    return _context.Movies.Any(e => e.Id == id);
        //}
    }
}
