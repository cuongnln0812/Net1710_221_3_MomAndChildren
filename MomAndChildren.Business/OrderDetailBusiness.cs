using Microsoft.EntityFrameworkCore;
using MomAndChildren.Data.Models;
using MomAndChildren.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Business
{
    public interface IOrderDetailBusiness
    {
        Task<IMomAndChildrenResult> GetOrderDetailsAsync();
        Task<IMomAndChildrenResult> GetOrderDetailByIdAsync(int orderDetailId);
        Task<IMomAndChildrenResult> CreateOrderDetail(int orderId, List<ProductCart> products);
    }

    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private readonly Net1710_221_3_MomAndChildrenContext _context;

        public OrderDetailBusiness(Net1710_221_3_MomAndChildrenContext context)
        {
            _context = context;
        }

        public async Task<IMomAndChildrenResult> CreateOrderDetail(int orderId, List<ProductCart> products)
        {
            //create new order details
            var orderDetails = new List<OrderDetail>();
            foreach (var item in products)
            {
                double price = item.Product.Price;
                int quantityCart = item.QuantityCart;

                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    UnitPrice = price,
                    TotalPrice = price * quantityCart,
                    ProductId = item.Product.ProductId,
                    Quantity = quantityCart,
                };
                orderDetails.Add(orderDetail);
            }
            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
            return new MomAndChildrenResult(1, "Create OrderDetails Successfully!");
        }

        public async Task<IMomAndChildrenResult> GetOrderDetailByIdAsync(int orderDetailId)
        {
            OrderDetail? orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail == null)
            {
                return new MomAndChildrenResult(-1, "OrderDetail not found");
            }
            else
            {
                return new MomAndChildrenResult(1, "Get OrderDetail success", orderDetail);
            }
        }

        public async Task<IMomAndChildrenResult> GetOrderDetailsAsync()
        {
            var orderDetails = await _context.OrderDetails.OrderByDescending(x => x.OrderId).ToListAsync();
            return new MomAndChildrenResult(1, "Get OrderDetails success", orderDetails);
        }
    }
}
