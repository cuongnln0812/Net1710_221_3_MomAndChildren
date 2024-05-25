using Microsoft.EntityFrameworkCore;
using MomAndChildren.Data.Base;
using MomAndChildren.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Data.DAO
{
    public class PaymentHistoryDAO : BaseDAO<PaymentHistory>
    {
        public PaymentHistoryDAO() : base() { }

        public async Task<List<PaymentHistory>> GetPaymentHistoryListByCustomerId(int id)
        {
            return await _context.PaymentHistories
                .Where(x => x.Order.CustomerId == id)
                .OrderByDescending(x => x.PaymentId)
                .ToListAsync();
        }

    }
}
