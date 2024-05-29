using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndChildren.Business;

namespace MomAndChildren.RazorWebApp.Pages
{
    public class PaymentHistoryModel : PageModel
    {

        private readonly IPaymentHistoryBusiness _paymentHistoryBusiness;

        public PaymentHistoryModel(IPaymentHistoryBusiness paymentHistoryBusiness)
        {
            _paymentHistoryBusiness = paymentHistoryBusiness;
        }

        public IMomAndChildrenResult result;

        public async Task OnGetAsync()
        {
            result = await _paymentHistoryBusiness.GetPaymentHistoryList();
        }
    }
}
