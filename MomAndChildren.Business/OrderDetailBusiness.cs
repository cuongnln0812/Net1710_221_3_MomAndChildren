using Microsoft.EntityFrameworkCore;
using MomAndChildren.Common;
using MomAndChildren.Data.DAO;
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
        private readonly OrderDetailDAO _DAO;

        public OrderDetailBusiness(Net1710_221_3_MomAndChildrenContext context=)
        {
            _DAO = new OrderDetailDAO();
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
            await _DAO.CreateOrderDetail(orderDetails);
            return new MomAndChildrenResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
        }

        public async Task<IMomAndChildrenResult> GetOrderDetailByIdAsync(int orderDetailId)
        {
            OrderDetail? orderDetail = await _DAO.GetByIdAsync(orderDetailId);
            if (orderDetail == null)
            {
                return new MomAndChildrenResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG);
            }
            else
            {
                return new MomAndChildrenResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetail);
            }
        }

        public async Task<IMomAndChildrenResult> GetOrderDetailsAsync()
        {
            var orderDetails = await _DAO.GetAllAsync();
            return new MomAndChildrenResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orderDetails);

        }
    }
}
