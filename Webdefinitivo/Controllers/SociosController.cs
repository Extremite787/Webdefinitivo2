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
        public IActionResult Index()
        {
            List<Socio> lissocios = new List<Socio>();
            lissocios = _context.Socio.ToList();
            return View(lissocios);
        }

        // GET: Socios/Details/5
        public IActionResult Details(string id)
        {
            Socio socio = _context.Socio.Where(x => x.Cedula == id).FirstOrDefault();
            return View(socio);        
        }

        // GET: Socios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Socios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Socio socio)
        {
            try
            {
                socio.Estado = 1;
                _context.Add(socio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch //(Exception)
            {
                return View(socio);
            }

        }

        // GET: Socios/Edit/5
        public IActionResult Edit(string id)
        {
            Socio socio = _context.Socio.Where(x => x.Cedula == id).FirstOrDefault();
            return View(socio);
        }

        // POST: Socios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Socio socio)
        {
            if (id != socio.Cedula)
                return RedirectToAction("Index");
            try
            {
                _context.Update(socio);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(socio);
            }
        }
        public IActionResult Desactivar(string id)
        {
            Socio socio = _context.Socio.Where(x => x.Cedula == id).FirstOrDefault();
            socio.Estado = 0;
            _context.Update(socio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Activar(string id)
        {
            Socio socio = _context.Socio.Where(x => x.Cedula == id).FirstOrDefault();
            socio.Estado = 1;
            _context.Update(socio);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
