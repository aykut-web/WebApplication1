using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DomainLayer.Entities.Concrete;
using WebApplication1.InfrastructureLayer.Context;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ProjectContext _context;

        public EmployeesController(ProjectContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var projectContext = _context.Employees.Include(e => e.Location);
        //    return View(await projectContext.ToListAsync());
        //}

        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees
        //        .Include(e => e.Location)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        //public IActionResult Create()
        //{
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
        //    return View();
        //}

      
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("LocationId,Title,Tckn,PhoneNumber,ImageFolder,Id,Name")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        employee.Id = Guid.NewGuid();
        //        _context.Add(employee);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", employee.LocationId);
        //    return View(employee);
        //}

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", employee.LocationId);
        //    return View(employee);
        //}

        
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("LocationId,Title,Tckn,PhoneNumber,ImageFolder,Id,Name")] Employee employee)
        //{
        //    if (id != employee.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(employee);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!EmployeeExists(employee.Id))
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
        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name", employee.LocationId);
        //    return View(employee);
        //}

        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees
        //        .Include(e => e.Location)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool EmployeeExists(Guid id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
    }
}
