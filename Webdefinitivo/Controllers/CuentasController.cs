﻿using System;
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
    public class CuentasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cuentas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cuenta.Include(c => c.CodigoSocioNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cuentas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuenta
                .Include(c => c.CodigoSocioNavigation)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // GET: Cuentas/Create
        public IActionResult Create()
        {
            ViewData["CodigoSocio"] = new SelectList(_context.Socio, "Cedula", "Cedula");
            return View();
        }

        // POST: Cuentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,SaldoTotal,CodigoSocio,Estado")] Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoSocio"] = new SelectList(_context.Socio, "Cedula", "Cedula", cuenta.CodigoSocio);
            return View(cuenta);
        }

        // GET: Cuentas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuenta.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }
            ViewData["CodigoSocio"] = new SelectList(_context.Socio, "Cedula", "Cedula", cuenta.CodigoSocio);
            return View(cuenta);
        }

        // POST: Cuentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Numero,SaldoTotal,CodigoSocio,Estado")] Cuenta cuenta)
        {
            if (id != cuenta.Numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentaExists(cuenta.Numero))
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
            ViewData["CodigoSocio"] = new SelectList(_context.Socio, "Cedula", "Cedula", cuenta.CodigoSocio);
            return View(cuenta);
        }

        // GET: Cuentas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuenta = await _context.Cuenta
                .Include(c => c.CodigoSocioNavigation)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return View(cuenta);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cuenta = await _context.Cuenta.FindAsync(id);
            _context.Cuenta.Remove(cuenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentaExists(string id)
        {
            return _context.Cuenta.Any(e => e.Numero == id);
        }
    }
}
