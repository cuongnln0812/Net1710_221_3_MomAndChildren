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
        private OrderRepository _order;
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
            get
            {
                return _paymentHistory ??= new Repositories.PaymentHistoryRepository();
            }
        }

        public OrderRepository OrderRepository 
        { 
            get
            { 
                return _order ??= new Repositories.OrderRepository(); 
            }
        }
    }
}
