using Microsoft.EntityFrameworkCore;
using MomAndChildren.Data.DAO;
using MomAndChildren.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Business
{
    public interface IBrandBusiness
    {
        Task<IMomAndChildrenResult> GetBrandsAsync();
        Task<IMomAndChildrenResult> GetBrandByIdAsync(int brandId);
        Task<IMomAndChildrenResult> CreateBrand(Brand brand);
        Task<IMomAndChildrenResult> UpdateBrand(Brand brand);
        Task<IMomAndChildrenResult> DeleteBrand(int brandId);
        Task<IMomAndChildrenResult> ChangeStatus(int brandId);
    }

    public class BrandBusiness : IBrandBusiness
    {
        private readonly BrandDAO _BrandDAO;
        private readonly ProductDAO _ProductDAO;

        public BrandBusiness()
        {
            _BrandDAO = new BrandDAO();
            _ProductDAO = new ProductDAO();
        }

        public async Task<IMomAndChildrenResult> CreateBrand(Brand brand)
        {
            try
            {
                //check trung ten
                var brands = await _BrandDAO.GetAllAsync();
                foreach (var item in brands)
                {
                    if (brand.BrandName.Equals(item.BrandName))
                    {
                        return new MomAndChildrenResult(-1, "Name is duplicated.");
                    }
                }
                brand.Status = 1;
                await _BrandDAO.CreateAsync(brand);

                return new MomAndChildrenResult(1, "Create the brand successfully");
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(-1, ex.Message);
            }

        }

        public async Task<IMomAndChildrenResult> DeleteBrand(int brandId)
        {
            try
            {
                var brand = await _BrandDAO.GetByIdAsync(brandId);
                if (brand != null)
                {
                    //xem bang product co brand muon xóa ko, có -> thì ko cho xóa; ko có -> chuyển status
                    var products = await _ProductDAO.GetAllAsync();
                    foreach (var product in products)
                    {
                        if (brandId == product.Category.CategoryId)
                        {
                            return new MomAndChildrenResult(-1, "Brand is using");
                        }
                    }
                    brand.Status = 0;
                    await _BrandDAO.UpdateAsync(brand);
                    return new MomAndChildrenResult(1, "Brand is inactive");
                }
                else
                {
                    return new MomAndChildrenResult(-1, "Brand is not exist");
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(-1, ex.Message);
            }
        }

        public async Task<IMomAndChildrenResult> ChangeStatus(int brandId)
        {
            try
            {
                var brand = await _BrandDAO.GetByIdAsync(brandId);
                if (brand != null)
                {
                    if (brand.Status == 0)
                    {
                        brand.Status = 1;
                        await _BrandDAO.UpdateAsync(brand);
                        return new MomAndChildrenResult(1, "Brand is active");
                    }
                    else
                    {
                        brand.Status = 0;
                        await _BrandDAO.UpdateAsync(brand);
                        return new MomAndChildrenResult(1, "Brand is inactive");
                    }
                }
                else
                {
                    return new MomAndChildrenResult(-1, "Brand is not exist");
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(-1, ex.Message);
            }
        }

        public async Task<IMomAndChildrenResult> GetBrandsAsync()
        {
            try
            {
                var brands = await _BrandDAO.GetAllAsync();

                if (brands == null)
                {
                    return new MomAndChildrenResult(-1, "Get brand list fail");
                }
                else
                {
                    return new MomAndChildrenResult(1, "Get brand list successfully", brands);
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(1, ex.Message);
            }
        }

        public async Task<IMomAndChildrenResult> GetBrandByIdAsync(int brandId)
        {
            try
            {
                var brand = await _BrandDAO.GetByIdAsync(brandId);
                if (brand == null)
                {
                    return new MomAndChildrenResult(-1, "Brand is not exist");
                }
                else
                {
                    return new MomAndChildrenResult(1, "Get brand success", brand);
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(-1, ex.Message);
            }
        }

        public async Task<IMomAndChildrenResult> UpdateBrand(Brand brand)
        {
            try
            {
                var newBrand = await _BrandDAO.GetByIdAsync(brand.BrandId);
                if (newBrand != null)
                {
                    newBrand.BrandName = brand.BrandName;
                    await _BrandDAO.UpdateAsync(newBrand);
                    return new MomAndChildrenResult(1, "Update brand success");
                }
                else
                {
                    return new MomAndChildrenResult(-1, "Brand is not exist");
                }
            }
            catch (Exception ex)
            {
                return new MomAndChildrenResult(-1, ex.Message);
            }
        }
    }
}
