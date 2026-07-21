using Brokerage.Application.orders.Commands.PlaceOrder;
using Brokerage.Application.Orders.Commands.CancelOrder;
using Brokerage.Application.Orders.Queries.GetOrders;
using Brokerage.Data;
using Brokerage.DTOs;
using Brokerage.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BrokerageFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersDTO>>> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());

            return Ok(orders);
        }
        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            var order = await _mediator.Send(
                new GetOrderByIdQuery(id));

            return Ok(order);
        }

        //POST: api/Orders
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(CreateOrdersDTO dto)
        {
            var command = new PlaceOrderCommand
            {
                Id = dto.Id,
                OrderType = dto.OrderType,
                LimitPrice = dto.LimitPrice,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity
            };

            var order = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetOrders),
                new { id = order.OrderId },
                order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var result = await _mediator.Send(
                new CancelOrderCommand(id));


            if (!result)
            {
                return NotFound();
            }


            return NoContent();
        }
    }
}
