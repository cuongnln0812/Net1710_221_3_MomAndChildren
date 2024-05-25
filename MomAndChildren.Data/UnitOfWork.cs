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

        private ProductRepository _productRepo;

        public UnitOfWork() { }

        public ProductRepository ProductRepository
        { 
            get { 
                return _productRepo ??= new Repositories.ProductRepository(); 
            } 
        }
    }
}
