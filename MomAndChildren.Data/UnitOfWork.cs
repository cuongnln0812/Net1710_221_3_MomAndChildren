using MomAndChildren.Data.Models;
using MomAndChildren.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Data
{
    public class UnitOfWork
    {
        private Net1710_221_3_MomAndChildrenContext _unitOfWorkContext;

        private OrderDetailRepository _orderDetail;

        private PaymentHistoryRepository _paymentHistory;

        public UnitOfWork() { }

        public OrderDetailRepository OrderDetailRepository
        { 
            get { 
                return _orderDetail ??= new Repositories.OrderDetailRepository(); 
            } 
        }

        public PaymentHistoryRepository PaymentHistoryRepository
        {
            get{
                return _paymentHistory ??= new Repositories.PaymentHistoryRepository();
            } 
        }
    }
}
