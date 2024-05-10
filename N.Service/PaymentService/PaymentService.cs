using Microsoft.AspNetCore.Http;
using N.Extensions;
using N.Model.Entities;
using N.Repository.ServiceFeeRepository;
using N.Service.BookingService;
using N.Service.Common.Service;
using N.Service.PaymentService;
using N.Service.ServiceFeeService.Dto;
using static N.Extensions.ConfigurationExtensions;

namespace N.Service.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IBookingService _bookingService;

        public PaymentService(IBookingService bookingService)
        {
            this._bookingService = bookingService;
        }

        public async Task<string> CreatePaymentUrl(Guid bookingId, string returlUrl, string ip)
        {
            var booking = (await _bookingService.GetDto(bookingId)).Data;
            if (booking != null)
            {

                var amount = booking.Price;

                if (booking.DepositValue.HasValue)
                {
                    amount = amount - booking.DepositValue;
                }

                if (!amount.HasValue || amount < 10000)
                {
                    amount = 10000;
                }

                var timeNow = DateTime.Now;
                var tick = timeNow.Ticks.ToString();
                var pay = new VnPayLibrary();
                pay.AddRequestData("vnp_Version", AppSettings.VnPay.Version);
                pay.AddRequestData("vnp_Command", AppSettings.VnPay.Command);
                pay.AddRequestData("vnp_TmnCode", AppSettings.VnPay.TmnCode);
                pay.AddRequestData("vnp_Amount", (amount * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", AppSettings.VnPay.CurrCode);
                pay.AddRequestData("vnp_IpAddr", ip);
                pay.AddRequestData("vnp_Locale", AppSettings.VnPay.Locale);
                pay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang");
                pay.AddRequestData("vnp_OrderType", "other");
                pay.AddRequestData("vnp_ReturnUrl", returlUrl);
                pay.AddRequestData("vnp_TxnRef", tick);

                var paymentUrl =
                    pay.CreateRequestUrl(AppSettings.VnPay.BaseUrl, AppSettings.VnPay.HashSecret);

                return paymentUrl;
            }
            return null;
        }

        public async Task<string> CreateDepositUrl(Guid bookingId, string returlUrl, string ip)
        {
            var booking = (await _bookingService.GetDto(bookingId)).Data;
            if (booking != null)
            {

                var amount = booking.Price / 30;
                if (!amount.HasValue || amount < 10000)
                {
                    amount = 10000;
                }

                var timeNow = DateTime.Now;
                var tick = timeNow.Ticks.ToString();
                var pay = new VnPayLibrary();
                pay.AddRequestData("vnp_Version", AppSettings.VnPay.Version);
                pay.AddRequestData("vnp_Command", AppSettings.VnPay.Command);
                pay.AddRequestData("vnp_TmnCode", AppSettings.VnPay.TmnCode);
                pay.AddRequestData("vnp_Amount", (amount * 100).ToString());
                pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", AppSettings.VnPay.CurrCode);
                pay.AddRequestData("vnp_IpAddr", ip);
                pay.AddRequestData("vnp_Locale", AppSettings.VnPay.Locale);
                pay.AddRequestData("vnp_OrderInfo", $"Dat coc don hang");
                pay.AddRequestData("vnp_OrderType", "other");
                pay.AddRequestData("vnp_ReturnUrl", returlUrl);
                pay.AddRequestData("vnp_TxnRef", tick);

                var paymentUrl =
                    pay.CreateRequestUrl(AppSettings.VnPay.BaseUrl, AppSettings.VnPay.HashSecret);

                booking.DepositValue = amount;
                await _bookingService.Update(booking);

                return paymentUrl;
            }
            return string.Empty;
        }
    }
}
