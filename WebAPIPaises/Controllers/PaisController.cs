﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIPaises.Models;

namespace WebAPIPaises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaisController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<Pais> Get()
        {
            return _context.Paises.ToList();
        }

        [HttpGet("{id}", Name = "paisCreado")]
        public IActionResult GetById(int id)
        {
            var pais = this._context.Paises.Include(p => p.Provincias).FirstOrDefault(p => p.Id == id);

            if (pais == null)
                return NotFound();

            return Ok(pais);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this._context.Paises.Add(pais);
            this._context.SaveChanges();

            return new CreatedAtRouteResult("paisCreado", new { id = pais.Id }, pais);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais pais, int id) 
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }

            this._context.Entry(pais).State = EntityState.Modified;
            this._context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pais = this._context.Paises.FirstOrDefault(p => p.Id == id);

            if (pais == null)
            {
                return NotFound();
            }

            this._context.Paises.Remove(pais);
            this._context.SaveChanges();

            return Ok(pais);
        }
    }
}