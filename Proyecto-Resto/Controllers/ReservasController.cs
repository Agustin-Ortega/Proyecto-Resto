using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Proyecto_Resto.Context;
using Proyecto_Resto.Models;
using Proyecto_Resto.Models.ViewModels;

namespace Proyecto_Resto.Controllers
{
    public class ReservasController : Controller
    {
        private readonly RestoContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReservasController(RestoContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> IndexReserva()
        {
            int usuariologueado = 1; // valor hardcodeado desp lo sacamos de identity
            // traemos las relaciones de foreing key
            var restoContext = await _context.Reservas
                                                    .Where(r => r.idCliente == usuariologueado)
                                                    .Include(r => r.Cliente)
                                                    .Include(r => r.Restaurante)
                                                    .Include(r => r.ItemReserva)
                                                    .OrderByDescending(p => p.Fecha)//?
                                                    .ToListAsync();

            // traemos la lista de platos
            foreach (var reserva in restoContext)
            {
                foreach (var item in reserva.ItemReserva)
                {
                    item.Plato = await _context.Platos.FindAsync(item.idPlato);
                }
            }

            return View(restoContext);
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            //int usuariologueado = 1; // valor hardcodeado desp lo sacamos de identity
            // traemos las relaciones de foreing key


            var user = await _userManager.GetUserAsync(User);

            var restoContext = await _context.Reservas
                                                    .Where(r=> r.Cliente.Email.ToUpper() == user.Email.Normalize())
                                                    .Include(r => r.Cliente)
                                                    .Include(r => r.Restaurante)
                                                    .Include(r=> r.ItemReserva)
                                                    .OrderByDescending(p => p.Fecha)//?
                                                    .ToListAsync();

            // traemos la lista de platos
            foreach (var reserva in restoContext)
            {
                foreach (var item in reserva.ItemReserva)
                {
                    item.Plato = await _context.Platos.FindAsync(item.idPlato);
                }
            }

            return View(restoContext);
        }

        // GET: Reservas de admi
        public async Task<IActionResult> IndexAdmin() 
        {            

            // el administrador va a poder ver todos los pedidos

            // traemos las relaciones de foreing key
            var restoContext = await _context.Reservas                                                    
                                                    .Include(r => r.Cliente)
                                                    .Include(r => r.Restaurante)
                                                    .Include(r => r.ItemReserva) //?
                                                    .ToListAsync();

            // traemos la lista de platos
            foreach (var reserva in restoContext)
            {
                foreach (var item in reserva.ItemReserva)
                {
                    item.Plato = await _context.Platos.FindAsync(item.idPlato);
                }
            }

            return View(restoContext);
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
                .Include(r => r.Restaurante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }




        // GET: Reservas/Create
        public async Task<IActionResult> Create()
        {

            int usuarioLogueado = 1;  //  valor hardcodeado desp lo sacamos de identity

            // creo la lista de item reservas

            var itemReservas = await _context.ItemReservas
                .Include(i => i.Reserva)
                .Include(i => i.Plato)
                .ToListAsync();

            // filtramos los platos del cliente
            //var listaItem = itemReservas.Where(p => p.idCliente == usuarioLogueado && p.ItemProcesado ==false).ToList();  esto si hacemos la relacion entre plato y cliente
            // filtramos los platos q ya se generaron
            var listaItem = itemReservas.Where(p => p.ItemProcesado == false).ToList();

            // calculamos el total de los items
            double total = ObtenerTotal(listaItem);


            // creamos el objeto reserva

            Reserva reserva = new Reserva
            {
                idCliente = usuarioLogueado,
                Cliente = await _context.Clientes.FindAsync(usuarioLogueado),
                Fecha = new DateTime(),   //?
                CantidadPersonas = 0,     //?
                idRestaurante = null,        //?
                Restaurante = null,       //?
                ItemReserva = listaItem,
                Total = total,

            };

            if(await CrearReserva(reserva))
            {
                //return RedirectToAction(nameof(Index));
                //return View(reserva);
                return RedirectToAction("IndexReserva", "Reservas");
            }

            return RedirectToAction("Privacy", "Home");  // seria en caso de error




            //ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "Apellido");
            //ViewData["idRestaurante"] = new SelectList(_context.Restaurantes, "Id", "nombre");
            //return View();
        }

        private async Task<bool> CrearReserva(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                // lo guardo
                _context.Add(reserva);
                await _context.SaveChangesAsync();

                // cambiamos el estado de los item a true indicando que ya se genero la reserva exitosamente
                foreach (var item in reserva.ItemReserva)
                {
                    item.ItemProcesado = true;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                return true;
            }

            return false;
        }





        // POST: Reservas/Create   NO USADO, EL GENERADOR DE RESERVAS SE HACE DESDE EL ITEM 

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Fecha,CantidadPersonas,idCliente,Total,idRestaurante")] Reserva reserva)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(reserva);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["idCliente"] = new SelectList(_context.Clientes, "Id", "Apellido", reserva.idCliente);
        //    ViewData["idRestaurante"] = new SelectList(_context.Restaurantes, "Id", "nombre", reserva.idRestaurante);
        //    return View(reserva);
        //}











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
            ViewData["idRestaurante"] = new SelectList(_context.Restaurantes, "Id", "nombre", reserva.idRestaurante);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,CantidadPersonas,idCliente,Total,idRestaurante")] Reserva reserva)
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

        private double ObtenerTotal(List<ItemReserva> lista)
        {
            double total = 0;

            foreach (var item in lista)
            {
                total += item.Plato.precio;
            }

            return total;
        }

        public async Task<IActionResult> ObtenerTicketMayor2()
        {

            var restoContext = await _context.Reservas
                                                    .Include(r => r.Cliente)
                                                    .Include(r => r.Restaurante)
                                                    .Include(r => r.ItemReserva)
                                                    .ThenInclude(p => p.Plato)
                                                    .OrderByDescending(p => p.Fecha)
                                                    .ToListAsync();

            Reserva mayor = null;
            double total = 0;

            foreach (var item in restoContext)
            {
                if (item.Total >= total)
                {
                    total = item.Total;
                    mayor = item;
                }

            }

            return View("MejorTicket", mayor);
        }


















    }
}
