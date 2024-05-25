using MomAndChildren.Data.Base;
using MomAndChildren.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Data.DAO
{
    public class OrderDetailDAO : BaseDAO<OrderDetail>
    {
        public OrderDetailDAO() { }
        
        public async Task CreateOrderDetail(List<OrderDetail> orderDetails)
        {
            await _dbSet.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }
    }
}
