using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomAndChildren.Data.Models;

namespace MomAndChildren.RazorWebApp.Pages.CustomerPage
{
    public class DetailsModel : PageModel
    {
        private readonly MomAndChildren.Data.Models.Net1710_221_3_MomAndChildrenContext _context;

        public DetailsModel(MomAndChildren.Data.Models.Net1710_221_3_MomAndChildrenContext context)
        {
            _context = context;
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
