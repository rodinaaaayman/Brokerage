using Brokerage.Data;
using Brokerage.DTOs;
using Brokerage.Models;
using Microsoft.AspNetCore.Mvc;
using static Brokerage.Models.Orders;

namespace Brokerage.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class ExecutionsController : ControllerBase
        {
            private readonly AppDbContext _context;

            public ExecutionsController(AppDbContext context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<IActionResult> PostExecution(CreateExecutionDTO dto)
            {
                var order = await _context.Orders.FindAsync(dto.OrderId);

                if (order == null)
                    return NotFound();

                if (order.FilledQuantity + dto.ExecutionQuantity > order.Quantity)
                    return UnprocessableEntity("Execution exceeds remaining quantity.");

                var execution = new Executions
                {
                    OrderId = dto.OrderId,
                    ExecutionQuantity = dto.ExecutionQuantity,
                    ExecutionDate = DateTime.UtcNow
                };

                _context.Executions.Add(execution);

                order.FilledQuantity += dto.ExecutionQuantity;

                if (order.FilledQuantity == order.Quantity)
                    order.Status = OrderStatus.Filled;
                else
                    order.Status = OrderStatus.PartiallyFilled;

                await _context.SaveChangesAsync();

                return Ok(execution);
            }
        }
    }

