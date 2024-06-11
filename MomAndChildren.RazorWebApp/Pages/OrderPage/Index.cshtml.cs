using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomAndChildren.Business;
using MomAndChildren.Data.Models;

namespace MomAndChildren.RazorWebApp.Pages.OrderPage
{
    public class IndexModel : PageModel
    {
        private readonly IOrderBusiness business;
        private readonly ICustomerBusiness customer;

        public IndexModel()
        {
            business ??= new OrderBusiness();
            customer ??= new CustomerBusiness();
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetOrdersAsync();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Order = result.Data as List<Order>;
            }
        }
    }
}
