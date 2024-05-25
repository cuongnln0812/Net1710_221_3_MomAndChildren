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
        private ProductRepository _productRepo;

        public UnitOfWork() { }

        //public OrderDetailRepository OrderDetailRepository
        //{
        //        return _orderDetail ??= new Repositories.OrderDetailRepository(); 
        //}

        public ProductRepository ProductRepository
        { 
            get { 
                return _productRepo ??= new Repositories.ProductRepository(); 
            } 
        }
    }
}
