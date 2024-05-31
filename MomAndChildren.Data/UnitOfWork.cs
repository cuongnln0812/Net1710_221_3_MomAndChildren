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
        private CategoryRepository _category;

        private ProductRepository _product;

        private BrandRepository _brand;

        public UnitOfWork() { }

        public OrderDetailRepository OrderDetailRepository
        { 
            get { 
                return _orderDetail ??= new Repositories.OrderDetailRepository(); 
            } 
        }

        #endregion
    }
}
