using Brokerage.Data;
using Brokerage.DTOs;
using Brokerage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Brokerage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientsDTO>>> GetClients()
        {
            var clients = await _context.Clients
                .Where(c => c.IsActive)
                .Select(c => new ClientsDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    NationalID = c.NationalID,
                    PhoneNumber = c.PhoneNumber,
                    AccountBalance = c.AccountBalance
                })
                .ToListAsync();

            return Ok(clients);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientsDTO>> GetClients(int id)
        {
            var client = await _context.Clients
                .Where(c => c.Id == id && c.IsActive)
                .Select(c => new ClientsDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    NationalID = c.NationalID,
                    PhoneNumber = c.PhoneNumber,
                    AccountBalance = c.AccountBalance
                })
                .FirstOrDefaultAsync();

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClients(int id, Clients clients)
        {
            if (id != clients.Id)
            {
                return BadRequest();
            }

            _context.Entry(clients).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientsExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clients>> PostClients(CreateClientsDTO dto)
        {
            var client = new Clients
            {
                Username = dto. Username,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                NationalID = dto.NationalID,
                PhoneNumber = dto.PhoneNumber
            };

            client.Deposit(dto.Deposit);

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClients), new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClients(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            // Soft delete
            client.IsActive = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
