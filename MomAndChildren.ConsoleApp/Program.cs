
using MomAndChildren.Business;
using MomAndChildren.Data.DAO;
using MomAndChildren.Data.Models;

Console.WriteLine("Hello, World!");

PaymentHistoryBusiness a = new();
Console.WriteLine(a.GetPaymentHistoryListByCustomerIdAsync(1));
