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

        private CategoryRepository _category;

        private ProductRepository _product;

        private BrandRepository _brand;

        public UnitOfWork() { }

        public CategoryRepository CategoryRepository
        {
            get { return _category ??= new Repository.CategoryRepository(); }
        }

        public ProductRepository ProductRepository
        {
            get { return _product ??= new Repository.ProductRepository(); }
        }

        public BrandRepository BrandRepository
        {
            get { return _brand ??= new Repository.BrandRepository(); }
        }
    }
}
