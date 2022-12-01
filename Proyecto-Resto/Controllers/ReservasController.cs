using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Resto.Context;
using Proyecto_Resto.Models;

namespace Proyecto_Resto.Controllers
{
    public class ReservasController : Controller
    {
        private readonly RestoContext _context;

        public ReservasController(RestoContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var restoContext = _context.Reservas.Include(r => r.Cliente).Include(r => r.Menu).Include(r => r.Restaurante);
            return View(await restoContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Menu)
                .Include(r => r.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "Apellido");
            ViewData["idMenu"] = new SelectList(_context.Menus, "Id", "Id");
            ViewData["idRestaurante"] = new SelectList(_context.Restaurantes, "Id", "nombre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,CantidadPersonas,idCliente,idMenu,idRestaurante")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.idCliente);
            ViewData["idMenu"] = new SelectList(_context.Menus, "Id", "Id", reserva.idMenu);
            ViewData["idRestaurante"] = new SelectList(_context.Restaurantes, "Id", "nombre", reserva.idRestaurante);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.idCliente);
            ViewData["idMenu"] = new SelectList(_context.Menus, "Id", "Id", reserva.idMenu);
            ViewData["idRestaurante"] = new SelectList(_context.Restaurantes, "Id", "nombre", reserva.idRestaurante);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,CantidadPersonas,idCliente,idMenu,idRestaurante")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.idCliente);
            ViewData["idMenu"] = new SelectList(_context.Menus, "Id", "Id", reserva.idMenu);
            ViewData["idRestaurante"] = new SelectList(_context.Restaurantes, "Id", "nombre", reserva.idRestaurante);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Menu)
                .Include(r => r.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'RestoContext.Reservas'  is null.");
            }
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
          return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
