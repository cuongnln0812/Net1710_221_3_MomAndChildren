using MomAndChildren.Data.Models;
using MomAndChildren.Data.Repository;
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

        private OrderRepository _order;

        public UnitOfWork()
        {
        }

        public OrderRepository OrderRepository 
        { 
            get
            { 
                return _order ??= new Repository.OrderRepository(); 
            }
        }
    }
}
