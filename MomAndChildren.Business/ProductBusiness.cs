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

        public async Task<IMomAndChildrenResult> CreateProduct(Product product)
        {
            if (_context.Products.Any(p => p.ProductId == product.ProductId))
            {
                return new MomAndChildrenResult(-1, "Product id is duplicate");
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return new MomAndChildrenResult(1, "Create product success", product);
        }

        public Task<IMomAndChildrenResult> DeleteProduct(int productId)
        {
            Product product = _context.Products.Find(productId);
            if (product == null)
            {
                return Task.FromResult<IMomAndChildrenResult>(new MomAndChildrenResult(-1, "Product not found"));
            }else
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Task.FromResult<IMomAndChildrenResult>(new MomAndChildrenResult(1, "Delete product success"));
            }
        }

        public Task<IMomAndChildrenResult> GetProductByIdAsync(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return Task.FromResult<IMomAndChildrenResult>(new MomAndChildrenResult(-1, "Product not found"));
            }else
            {
                return Task.FromResult<IMomAndChildrenResult>(new MomAndChildrenResult(1, "Get product success", product));
            }
        }

        public Task<IMomAndChildrenResult> GetProductsAsync()
        {
            List<Product> products = _context.Products.ToList();
            return Task.FromResult<IMomAndChildrenResult>(new MomAndChildrenResult(1, "Get products success", products));
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        public Task<IMomAndChildrenResult> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return Task.FromResult<IMomAndChildrenResult>(new MomAndChildrenResult(1, "Update product success", product));
        }
    }
}
