using Brokerage.Data;
using Brokerage.DTOs;
using Brokerage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Find the order
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == dto.OrderId);

            if (order == null)
                return NotFound("Order not found.");


            // Prevent over-fill
            if (order.FilledQuantity + dto.ExecutionQuantity > order.Quantity)
            {
                return UnprocessableEntity(
                    "Execution exceeds remaining quantity."
                );
            }


            // Create execution
            var execution = new Executions
            {
                OrderId = dto.OrderId,
                ExecutionQuantity = dto.ExecutionQuantity,
                ExecutionDate = DateTime.UtcNow
            };

            _context.Executions.Add(execution);


            // Update filled quantity
            order.FilledQuantity += dto.ExecutionQuantity;


            // If completely filled
            if (order.FilledQuantity == order.Quantity)
            {
                order.Status = OrderStatus.Filled;


                // Check if invoice already exists
                var invoiceExists = await _context.Invoices
                    .AnyAsync(i => i.OrderId == order.OrderId);


                // Create invoice only once
                if (!invoiceExists)
                {
                    var tradeValue = order.Quantity * order.UnitPrice;

                    var commission = tradeValue * order.CommissionRate;

                    var tax = 0m;
                    // TODO: replace with your tax calculation rule


                    var invoice = new Invoice
                    {
                        OrderId = order.OrderId,
                        TradeValue = tradeValue,
                        Commission = commission,
                        Tax = tax,
                        Total = tradeValue + commission + tax,
                        CreatedDate = DateTime.Now
                    };


                    _context.Invoices.Add(invoice);
                }
            }
            else
            {
                order.Status = OrderStatus.PartiallyFilled;
            }


            await _context.SaveChangesAsync();


            // Avoid circular reference problem
            return Ok(new
            {
                execution.ExecutionId,
                execution.OrderId,
                execution.ExecutionQuantity,
                execution.ExecutionDate,
                OrderStatus = order.Status
            });
        }
    }
}
