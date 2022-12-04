using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Resto.Context;
using Proyecto_Resto.Models;
using Proyecto_Resto.Models.ViewModels;

namespace Proyecto_Resto.Controllers
{
    public class ClientesController : Controller
    {
        private readonly RestoContext _context;

        public ClientesController(RestoContext context)
        {
            _context = context;
        }

        // GET: Clientes
        [HttpGet("Clientes/Inicio")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        [HttpGet("Clientes/Detalle/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,password,isAdmin")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
               //if (cliente.Apellido == "ortega")
               //{
               //    cliente.isAdmin = true;
               //}
               //RedirectToAction("Privacy", "Home");
               //var cliente2 = await _context.Clientes.OrderByDescending(e => e.Id).FirstOrDefaultAsync();

                


                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,password,isAdmin")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'RestoContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return _context.Clientes.Any(e => e.Id == id);
        }


        //VIEW MODEL DE ADMINISTRADOR

        public async Task<IActionResult> Informe1()
        {
            var listaReservas = await _context.Reservas
                .Where(p => p.Fecha >= DateTime.Now.AddDays(-30))
                .Include(p => p.Cliente)
                .Include(p => p.ItemReserva)
                .ThenInclude(i => i.Plato)  // de item pedido para no hacer un for each
                .OrderByDescending(p => p.Fecha)
                .ToListAsync();

            if (listaReservas == null)
            {
                return RedirectToAction("Privacy", "Home");

            }

            List<Informe1> listaDeInformes = new List<Informe1>();

            foreach(var reserva in listaReservas)
            {
                var informe1 = new Informe1
                {

                    Fecha = reserva.Fecha,  
                    Cliente = reserva.Cliente.Nombre + " " + reserva.Cliente.Apellido,
                    ListaPlatos = ObtenerPlatos(reserva),
                    Total = reserva.Total,
                    Ganancia = CalcularGanacia(reserva.Total, reserva)
                };
                listaDeInformes.Add(informe1);

            }

            return View(listaDeInformes); 


        }

        private double CalcularGanacia(double total, Reserva? reserva)
        {
            return total+= reserva.ItemReserva.Sum(i => i.Plato.precio);
            
        }

        private List<string> ObtenerPlatos(Reserva? reserva)
        {
            var lista = new List<string>();
            foreach (var plato in reserva.ItemReserva)
            {
                lista.Add(plato.Plato.nombre);
            }

            return lista;
        }
    }
}
