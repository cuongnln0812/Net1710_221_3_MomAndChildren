using MomAndChildren.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Business
{

    public interface IProductBusiness
    {
        Task<IMomAndChildrenResult> GetProductsAsync();
        Task<IMomAndChildrenResult> GetProductByIdAsync(int id);
        Task<IMomAndChildrenResult> CreateProduct(Product product);
        Task<IMomAndChildrenResult> UpdateProduct(Product product);
        Task<IMomAndChildrenResult> DeleteProduct(int productId);
        bool ProductExists(int id);
    }

    public class ProductBusiness : IProductBusiness
    {
        private readonly Net1710_221_3_MomAndChildrenContext _context;

        public ProductBusiness(Net1710_221_3_MomAndChildrenContext context)
        {
            _context = context;
        }

        public Task<IMomAndChildrenResult> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IMomAndChildrenResult> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IMomAndChildrenResult> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IMomAndChildrenResult> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public bool ProductExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IMomAndChildrenResult> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
