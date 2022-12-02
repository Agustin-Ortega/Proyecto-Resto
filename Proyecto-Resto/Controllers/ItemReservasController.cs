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
    public class ItemReservasController : Controller
    {
        private readonly RestoContext _context;

        public ItemReservasController(RestoContext context)
        {
            _context = context;
        }

        // GET: ItemReservas
        public async Task<IActionResult> Index()
        {
            var restoContext = await _context.ItemReservas.Include(i => i.Plato).Include(i => i.Reserva).ToListAsync();

            // listamos al item siempre que no este procesado, pendiente para generar la reserva
            var lista = restoContext.Where(i => i.ItemProcesado == false).ToList();

            return View(lista);
        }

        // GET: ItemReservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ItemReservas == null)
            {
                return NotFound();
            }

            var itemReserva = await _context.ItemReservas
                .Include(i => i.Plato)
                .Include(i => i.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemReserva == null)
            {
                return NotFound();
            }

            return View(itemReserva);
        }

        // GET: ItemReservas/Create
        public IActionResult Create()
        {
            ViewData["idPlato"] = new SelectList(_context.Platos, "Id", "nombre");
            //ViewData["idReserva"] = new SelectList(_context.Reservas, "Id", "Id");
            return View();
        }

        // POST: ItemReservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,idReserva,idPlato")] ItemReserva itemReserva)
        {
            if (ModelState.IsValid)
            {
                itemReserva.ItemProcesado = false; // por defecto viene sin procesar para que luego de generar la reserva se ponga en true y salga del estado pendiente de reserva
                _context.Add(itemReserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //itemReserva.ItemProcesado = true;
            //ViewData["idReserva"] = new SelectList(_context.Reservas, "Id", "Id", itemReserva.idReserva);
            //var lista = await _context.Platos.Where(p => p.stock > 0).ToListAsync();  validacion de mas 
            ViewData["idPlato"] = new SelectList(_context.Platos, "Id", "descricpion", itemReserva.idPlato);            
            return View(itemReserva);
        }

        // GET: ItemReservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemReservas == null)
            {
                return NotFound();
            }

            var itemReserva = await _context.ItemReservas.FindAsync(id);
            if (itemReserva == null)
            {
                return NotFound();
            }
            ViewData["idPlato"] = new SelectList(_context.Platos, "Id", "descricpion", itemReserva.idPlato);
            ViewData["idReserva"] = new SelectList(_context.Reservas, "Id", "Id", itemReserva.idReserva);
            return View(itemReserva);
        }

        // POST: ItemReservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemProcesado,idReserva,idPlato")] ItemReserva itemReserva)
        {
            if (id != itemReserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemReserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemReservaExists(itemReserva.Id))
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
            ViewData["idPlato"] = new SelectList(_context.Platos, "Id", "descricpion", itemReserva.idPlato);
            ViewData["idReserva"] = new SelectList(_context.Reservas, "Id", "Id", itemReserva.idReserva);
            return View(itemReserva);
        }

        // GET: ItemReservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ItemReservas == null)
            {
                return NotFound();
            }

            var itemReserva = await _context.ItemReservas
                .Include(i => i.Plato)
                .Include(i => i.Reserva)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemReserva == null)
            {
                return NotFound();
            }

            return View(itemReserva);
        }

        // POST: ItemReservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ItemReservas == null)
            {
                return Problem("Entity set 'RestoContext.ItemReservas'  is null.");
            }
            var itemReserva = await _context.ItemReservas.FindAsync(id);
            if (itemReserva != null)
            {
                _context.ItemReservas.Remove(itemReserva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemReservaExists(int id)
        {
          return _context.ItemReservas.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<Plato?> ObtenerFoto(int id)
        {
            var plato = await _context.Platos.FindAsync(id);
            return plato;
        }
    }
}
