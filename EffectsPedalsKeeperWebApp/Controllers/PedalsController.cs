using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EffectsPedalsKeeperShared.Data;
using EffectsPedalsKeeperShared.Models;
using EffectsPedalsKeeperShared.Models.ViewModels;

namespace EffectsPedalsKeeperWebApp.Controllers
{
    public class PedalsController : Controller
    {
        private readonly EffectsPedalsContext _context;

        public PedalsController(EffectsPedalsContext context)
        {
            _context = context;
        }

        // GET: Pedals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pedals.ToListAsync());
        }

        // GET: Pedals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedal = await _context.Pedals
                .Include(p => p.Settings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedal == null)
            {
                return NotFound();
            }

            var viewModel = new PedalDetailsViewModel(pedal);

            return View(viewModel);
        }

        // GET: Pedals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pedals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Maker,EffectType,Name")] Pedal pedal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedal);
        }

        // GET: Pedals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedal = await _context.Pedals.FindAsync(id);
            if (pedal == null)
            {
                return NotFound();
            }
            return View(pedal);
        }

        // POST: Pedals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Maker,EffectType,Name")] Pedal pedal)
        {
            if (id != pedal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedalExists(pedal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedal);
        }

        // GET: Pedals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedal = await _context.Pedals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedal == null)
            {
                return NotFound();
            }

            return View(pedal);
        }

        // POST: Pedals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedal = await _context.Pedals.FindAsync(id);
            _context.Pedals.Remove(pedal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedalExists(int id)
        {
            return _context.Pedals.Any(e => e.Id == id);
        }
    }
}
