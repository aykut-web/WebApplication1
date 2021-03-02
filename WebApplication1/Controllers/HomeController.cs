using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.ApplicationLayer.Mapper.Infrastructure;
using WebApplication1.InfrastructureLayer.Context;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMappedLocation _locationRepo;
        private readonly IMappedMovie _movieRepo;

        public HomeController(ILogger<HomeController> logger, IMappedLocation locationRepo, IMappedMovie movieRepo)
        {
            this._logger = logger;
            this._locationRepo = locationRepo;
            this._movieRepo = movieRepo;
            
        }

        public IActionResult Index()
        {
            var lc = _locationRepo.GetAllMappedLocations().ToList();
            var mv = _movieRepo.GetAllMappedMovies().ToList();
            //var mv = _context.Movies.ToList();
            var tuple = Tuple.Create(lc, mv);
            return View(tuple);


        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
