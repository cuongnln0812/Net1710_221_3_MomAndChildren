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
    public class IndexModel : PageModel
    {
        private readonly MomAndChildren.Data.Models.Net1710_221_3_MomAndChildrenContext _context;

        public IndexModel(MomAndChildren.Data.Models.Net1710_221_3_MomAndChildrenContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customers != null)
            {
                Customer = await _context.Customers.ToListAsync();
            }
        }
    }
}
