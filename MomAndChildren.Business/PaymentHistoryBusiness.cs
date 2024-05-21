using Microsoft.EntityFrameworkCore;
using MomAndChildren.Data.Models;

namespace MomAndChildren.Business
{

    public interface IPaymentHistoryBusiness
    {
        Task<IMomAndChildrenResult> GetPaymentHistoryListAsync();
        Task<IMomAndChildrenResult> GetPaymentHistoryByIdAsync(int id);
        Task<IMomAndChildrenResult> CreatePaymentHistory(Order order, string method);
        Task<IMomAndChildrenResult> UpdatePaymentStatus(int id, int status);
    }

    public class PaymentHistoryBusiness : IPaymentHistoryBusiness
    {
        private readonly Net1710_221_3_MomAndChildrenContext _context;

        public PaymentHistoryBusiness(Net1710_221_3_MomAndChildrenContext context)
        {
            _context = context;
        }

        public async Task<IMomAndChildrenResult> CreatePaymentHistory(Order order, String method)
        {
            var paymentHistory = new PaymentHistory
            {
                OrderId = order.OrderId,
                Order = order
            };
            if (method.Equals("ONLINE_BANKING"))
            {
                paymentHistory.Status = 1;
                paymentHistory.PurchaseDate = DateTime.Now;
                paymentHistory.PaymentMethod = method;
            }else if (method.Equals("COD"))
            {
                paymentHistory.Status = 0;
                paymentHistory.PaymentMethod = method;
            }
            else
            {
                return new MomAndChildrenResult(0, "Payment method: " + method + " not available");
            }
            _context.PaymentHistories.Add(paymentHistory);
            await _context.SaveChangesAsync();

            return new MomAndChildrenResult(1, "Payment completed", paymentHistory);
        }

        public async Task<IMomAndChildrenResult> GetPaymentHistoryByIdAsync(int id)
        {
            var payment = await _context.PaymentHistories.FindAsync(id);
            if(payment == null) return new MomAndChildrenResult(0, "Payment not found with id: " + id);
            else return new MomAndChildrenResult(1, "Get Payment by id: " + id + " successfully", payment);
        }

        public async Task<IMomAndChildrenResult> GetPaymentHistoryListAsync()
        {
            var paymentList = await _context.PaymentHistories.ToListAsync();
            return new MomAndChildrenResult(1, "Retrieve Payment list successfully", paymentList);
        }

        public async Task<IMomAndChildrenResult> UpdatePaymentStatus(int id, int status)
        {
            var payment = await _context.PaymentHistories.FindAsync(id);
            if (payment == null) return new MomAndChildrenResult(0, "Payment not found with id: " + id);
            if (payment.Status == 0 && status == 1)
            {
                payment.Status = status;
                payment.Status = status;
                _context.PaymentHistories.Update(payment);
                await _context.SaveChangesAsync();
            }
            else if (payment.Status == 1 && status == 0)
            {
                return new MomAndChildrenResult(0, "Cannot update status with status: " + status);
            }
            else return new MomAndChildrenResult(0, "Status invalid");

            return new MomAndChildrenResult(1, "Payment update successfully with id: " + id);
        }
    }
}
