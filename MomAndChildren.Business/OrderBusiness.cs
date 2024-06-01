using Microsoft.EntityFrameworkCore;
using MomAndChildren.Common;
using MomAndChildren.Data;
using MomAndChildren.Data.DAO;
using MomAndChildren.Data.Models;
using MomAndChildren.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Business
{
    public interface IOrderBusiness
    {
        Task<IMomAndChildrenResult> CreateOrder(int customerId, List<ProductCart> productCarts);
        Task<IMomAndChildrenResult> GetOrdersAsyncByCustomerId(int customerId);
        Task<IMomAndChildrenResult> GetAllOrdersWherePaymentIsNotNullAsync();
    }

    public class OrderBusiness : IOrderBusiness
    {

        //private readonly OrderDAO _DAO;

        private readonly UnitOfWork _unitOfWork;
        public OrderBusiness()
        {
            //_DAO = new OrderDAO();
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<IMomAndChildrenResult> GetOrdersAsyncByCustomerId(int customerId)
        {
            try
            {
                // var orders = await _context.Orders
                //    .Where(o => o.CustomerId == customerId)
                //  .Include(o => o.Customer)
                // .Include(o => o.OrderDetails)
                // .Include(o => o.PaymentHistories)
                //.ToListAsync();

                //var orders = await _DAO.GetOrdersByCustomerIdAsync(customerId);
                //chỉnh lại
                //var orders = await _unitOfWork.OrderRepository.GetAllAsync();

                var orders = await _unitOfWork.OrderRepository.GetOrdersByCustomerIdAsync(customerId);

                //return new MomAndChildrenResult(1, " Get all orders successfully", orders);
                return new MomAndChildrenResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orders);
            }catch (Exception ex)
            {
                return new MomAndChildrenResult(Const.FAIL_READ_CODE, Const.WARNING_NO_DATA__MSG);
            }
        }

        public async Task<IMomAndChildrenResult> CreateOrder(int customerId, List<ProductCart> productCarts)
        {
            try
            {
                double totalPrice = 0;
                int totalQuantity = 0;

                foreach(var productCart in productCarts)
                {
                    totalPrice += productCart.UnitPrice * productCart.QuantityCart;
                    totalQuantity += productCart.QuantityCart;
                }

                var order = new Order()
                {
                    CustomerId = customerId,
                    TotalPrice = totalPrice,
                    OrderDate = DateTime.Now,
                    TotalQuantity = totalQuantity,
                    OrderDetails = productCarts.Select(p => new OrderDetail()
                    {
                        ProductId = p.Product.ProductId,
                        Quantity = p.QuantityCart,
                        UnitPrice = p.UnitPrice
                    }).ToList()
                };

                //await _context.Orders.AddAsync(order);
                //await _context.SaveChangesAsync();

                //_DAO.CreateAsync(order);
                int result =await _unitOfWork.OrderRepository.CreateAsync(order);

                if (result > 0 )
                {
                    return new MomAndChildrenResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new MomAndChildrenResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }

               
            }catch(Exception ex)
            {
                return new MomAndChildrenResult(-1, ex.Message);
            }
        }

        public async Task<IMomAndChildrenResult> GetAllOrdersWherePaymentIsNotNullAsync()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetOrdersWherePaymentIsNotNullAsync();

                return new MomAndChildrenResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, orders);
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(Const.FAIL_READ_CODE, Const.WARNING_NO_DATA__MSG);
            }
        }
    }
}
