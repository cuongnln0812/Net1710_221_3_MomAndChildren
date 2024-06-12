using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomAndChildren.Business;
using MomAndChildren.Data.Models;

namespace MomAndChildren.RazorWebApp.Pages.CustomerPage
{
    public class IndexModel : PageModel
    {

        private readonly ICustomerBusiness business;
        //private readonly ICustomerBusiness customer;

        public IndexModel()
        {
            business ??= new CustomerBusiness();
            //customer ??= new CustomerBusiness();
        }

        public IList<Customer> Customers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetCustomersAsync();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                Customers = result.Data as List<Customer>;
            }
        }
    }
}
