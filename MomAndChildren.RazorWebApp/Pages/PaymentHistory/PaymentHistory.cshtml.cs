using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MomAndChildren.Business;

namespace MomAndChildren.RazorWebApp.Pages.PaymentHistory
{
    public class PaymentHistoryModel : PageModel
    {
        private readonly PaymentHistoryBusiness _paymentHistoryBusiness;
       



        public PaymentHistoryModel(PaymentHistoryBusiness paymentHistoryBusiness)
        {
            _paymentHistoryBusiness = paymentHistoryBusiness;
        }

        public async Task OnGetAsync()
        {
            var result = await _paymentHistoryBusiness.GetPaymentHistoryList();
            if(result.Status > 0)
            {
                
            }
        }
    }
}
