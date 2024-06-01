﻿using Microsoft.EntityFrameworkCore;
using MomAndChildren.Data.Base;
using MomAndChildren.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository()
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersWherePaymentIsNotNullAsync()
        {
            return await _context.Orders.Where(o => o.PaymentHistories != null && o.OrderDetails.Count > 0 ).ToListAsync();
        }
    }
}
