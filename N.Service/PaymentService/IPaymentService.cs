using Microsoft.AspNetCore.Http;
using N.Model.Entities;
using N.Service.Common.Service;
using N.Service.ServiceFeeService.Dto;

namespace N.Service.PaymentService
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentUrl(Guid bookingId, string returnUrl, string ip);
        //PaymentResponseModel PaymentExecute(IQueryCollection collections);
        Task<string> CreateDepositUrl(Guid bookingId, string returlUrl, string ip);
    }
}
