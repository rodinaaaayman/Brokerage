using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Brokerage.Models.Orders;

namespace Brokerage.Application.DTOs
{
        public class OrderSummaryDto
        {
            public int OrderId { get; set; }
        [ForeignKey("Client")]
            public int Id { get; set; }
            
            public string OrderType { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public string Status { get; set; }
        }
    }

