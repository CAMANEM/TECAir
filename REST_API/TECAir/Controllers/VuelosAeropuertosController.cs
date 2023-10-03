﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TECAir.Models;

namespace TECAir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosAeropuertosController : ControllerBase
    {
        private readonly TecairDbContext _context;

        public VuelosAeropuertosController(TecairDbContext context)
        {
            _context = context;
        }

        // GET: api/VuelosAeropuertos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VueloAeropuerto>>> GetVueloAeropuertos()
        {
          if (_context.VueloAeropuertos == null)
          {
              return NotFound();
          }
            return await _context.VueloAeropuertos.ToListAsync();
        }

        // GET: api/VuelosAeropuertos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VueloAeropuerto>> GetVueloAeropuerto(string id)
        {
          if (_context.VueloAeropuertos == null)
          {
              return NotFound();
          }
            var vueloAeropuerto = await _context.VueloAeropuertos.FindAsync(id);

            if (vueloAeropuerto == null)
            {
                return NotFound();
            }

            return vueloAeropuerto;
        }

        // PUT: api/VuelosAeropuertos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVueloAeropuerto(string id, VueloAeropuerto vueloAeropuerto)
        {
            if (id != vueloAeropuerto.AeropuertoId)
            {
                return BadRequest();
            }

            _context.Entry(vueloAeropuerto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VueloAeropuertoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VuelosAeropuertos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VueloAeropuerto>> PostVueloAeropuerto(VueloAeropuerto vueloAeropuerto)
        {
            if (_context.VueloAeropuertos == null)
            {
                return Problem("Entity set 'TecairDbContext.VueloAeropuertos'  is null.");
            }

            // Validar que el Aeropuerto existe
            var aeropuertoExistente = await _context.Aeropuertos.FindAsync(vueloAeropuerto.AeropuertoId);
            if (aeropuertoExistente == null)
            {
                return BadRequest("El avión especificado no existe.");
            }

            // Validar que el Vuelo existe
            var vueloExistente = await _context.Vuelos.FindAsync(vueloAeropuerto.VueloNumero);
            if (vueloExistente == null)
            {
                return BadRequest("El empleado especificado no existe.");
            }

            try
            {
                // Asignar el aeropuerto y vuelo existente a las propiedades de navegación
                vueloAeropuerto.Aeropuerto = aeropuertoExistente;
                vueloAeropuerto.VueloNumeroNavigation = vueloExistente;

                // Agregar el VueloAeropuerto a la base de datos
                _context.VueloAeropuertos.Add(vueloAeropuerto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }

            return Ok(JsonSerializer.Serialize(vueloAeropuerto, jsonSerializerOptions));
        }

        // DELETE: api/VuelosAeropuertos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVueloAeropuerto(string id)
        {
            if (_context.VueloAeropuertos == null)
            {
                return NotFound();
            }
            var vueloAeropuerto = await _context.VueloAeropuertos.FindAsync(id);
            if (vueloAeropuerto == null)
            {
                return NotFound();
            }

            _context.VueloAeropuertos.Remove(vueloAeropuerto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VueloAeropuertoExists(string id)
        {
            return (_context.VueloAeropuertos?.Any(e => e.AeropuertoId == id)).GetValueOrDefault();
        }

        private static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve };
    }
}
