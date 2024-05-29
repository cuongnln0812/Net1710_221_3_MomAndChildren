using Microsoft.EntityFrameworkCore;
using MomAndChildren.Common;
using MomAndChildren.Data;
using MomAndChildren.Data.DAO;
using MomAndChildren.Data.Models;


namespace MomAndChildren.Business
{
    public interface IPaymentHistoryBusiness
    {
        Task<IMomAndChildrenResult> GetPaymentHistoryList();
        Task<IMomAndChildrenResult> GetPaymentHistoryListByCustomerIdAsync(int id);
        Task<IMomAndChildrenResult> GetPaymentHistoryByIdAsync(int id);
        Task<IMomAndChildrenResult> CreatePaymentHistory(Order order, string method);
        Task<IMomAndChildrenResult> UpdatePayment(PaymentHistory paymentHistory);
    }

    public class PaymentHistoryBusiness : IPaymentHistoryBusiness
    {
        /*private readonly Net1710_221_3_MomAndChildrenContext _context;
        private readonly PaymentHistoryDAO _DAO;*/
        private readonly UnitOfWork _unitOfWork;

        public PaymentHistoryBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }


        public async Task<IMomAndChildrenResult> CreatePaymentHistory(Order order, string method)
        {
            try
            {
                var paymentHistory = new PaymentHistory
                {
                    OrderId = order.OrderId,
                    Order = order,
                    Status = 1,
                    PurchaseDate = DateTime.Now,
                    PaymentMethod = method
                };
                _unitOfWork.PaymentHistoryRepository.PrepareCreate(paymentHistory);
                int result = _unitOfWork.PaymentHistoryRepository.Save();
                if (result > 0)
                {
                    return new MomAndChildrenResult(Const.SUCCESS_CREATE_CODE, Const.SUCCESS_CREATE_MSG);
                }
                else
                {
                    return new MomAndChildrenResult(Const.FAIL_CREATE_CODE, Const.FAIL_CREATE_MSG);
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IMomAndChildrenResult> GetPaymentHistoryByIdAsync(int id)
        {
            try
            {
                var payment = _unitOfWork.PaymentHistoryRepository.GetByIdAsync(id);
                if (payment == null) return new MomAndChildrenResult(Const.WARNING_NO_DATA_CODE, "Payment not found with id: " + id);
                else return new MomAndChildrenResult(Const.SUCCESS_READ_CODE, "Get Payment by id: " + id + " successfully", payment);
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IMomAndChildrenResult> GetPaymentHistoryListByCustomerIdAsync(int id)
        {
            try
            {
                var paymentList = await _unitOfWork.PaymentHistoryRepository.GetPaymentHistoryListByCustomerId(id);
                if (paymentList == null)
                {
                    return new MomAndChildrenResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MomAndChildrenResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, paymentList);
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(Const.ERROR_EXCEPTION, ex.ToString());
            }
        }

        public async Task<IMomAndChildrenResult> GetPaymentHistoryList()
        {
            try
            {
                var list = await _unitOfWork.PaymentHistoryRepository.GetAllAsync();
                if (list == null)
                {
                    return new MomAndChildrenResult(Const.WARNING_NO_DATA_CODE, Const.WARNING_NO_DATA__MSG);
                }
                else
                {
                    return new MomAndChildrenResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, list);
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
