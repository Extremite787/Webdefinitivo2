using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webdefinitivo.Data;
using Webdefinitivo.Models;

namespace Webdefinitivo.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Socios
        public async Task<IActionResult> Index()
        {
            List<Socio> socios = new List<Socio>();
            socios = _context.Socio.ToList();
            return View(socios);
        }

        // GET: Socios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socio
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // GET: Socios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Socios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cedula,Nombre,Apellido,Direccion,Estado")] Socio socio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socio);
        }

        // GET: Socios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socio.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }
            return View(socio);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,Socio socio)
        {
            if (id != socio.Cedula)
                return RedirectToAction("Index");
            try
            {
                _context.Update(socio);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return View(socio);
            }
            return RedirectToAction("Index");
        }

        // GET: Socios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socio
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // POST: Socios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var socio = await _context.Socio.FindAsync(id);
            _context.Socio.Remove(socio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocioExists(string id)
        {
            return _context.Socio.Any(e => e.Cedula == id);
        }
        public IActionResult Desactivar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            Socio socio = _context.Socio.Where(x => x.Cedula == id).FirstOrDefault();
            try
            {
                socio.Estado = 0;
                _context.Update(socio);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Activar(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Index");
            Socio socio = _context.Socio.Where(x => x.Cedula == id).FirstOrDefault();
            try
            {
                socio.Estado = 1;
                _context.Update(socio);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
