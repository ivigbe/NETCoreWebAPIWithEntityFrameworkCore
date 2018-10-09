using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIPaises.Models;

namespace WebAPIPaises.Controllers
{
    [Route("api/Pais/{PaisId}/Provincia")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> Get(int PaisId)
        {
            return _context.Provincias.Where(p => p.PaisId == PaisId).ToList();
        }

        [HttpGet("{id}", Name = "provinciaCreada")]
        public IActionResult GetById(int id)
        {
            var provincia = this._context.Provincias.FirstOrDefault(p => p.Id == id);

            if (provincia == null)
                return NotFound();

            return Ok(provincia);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Provincia provincia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._context.Provincias.Add(provincia);
            this._context.SaveChanges();

            return new CreatedAtRouteResult("provinciaCreada", new { id = provincia.Id }, provincia);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Provincia provincia, int id)
        {
            if (provincia.Id != id)
            {
                return BadRequest();
            }

            this._context.Entry(provincia).State = EntityState.Modified;
            this._context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var provincia = this._context.Provincias.FirstOrDefault(p => p.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            this._context.Provincias.Remove(provincia);
            this._context.SaveChanges();

            return Ok(provincia);
        }
    }
}