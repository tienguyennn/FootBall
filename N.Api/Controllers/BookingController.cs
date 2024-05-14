using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.BookingService;
using N.Service.BookingService.Dto;
using N.Service.Common;
using N.Service.Constant;
using N.Service.Dto;
using N.Service.FieladService;
using N.Service.FieldService.Dto;
using N.Service.FieldServiceFeeService;
using N.Service.PaymentService;
using N.Service.ServiceFeePaymentService;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : NController
    {
        private readonly IBookingService _bookingService;
        private readonly IFieldService _fieldService;
        private readonly IFieldServiceFeeService _fieldServiceFeeService;
        private readonly IServiceFeePaymentService _serviceFeePaymentService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingController> _logger;


        public BookingController(
            IBookingService bookingService,
            IFieldService fieldService,
            IFieldServiceFeeService fieldServiceFeeService,
            IServiceFeePaymentService serviceFeePaymentService,
            IPaymentService paymentService,
            IMapper mapper,
            ILogger<BookingController> logger
            )
        {
            this._bookingService = bookingService;
            this._fieldService = fieldService;
            this._fieldServiceFeeService = fieldServiceFeeService;
            this._serviceFeePaymentService = serviceFeePaymentService;
            this._paymentService = paymentService;
            this._mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Create")]
        public async Task<DataResponse<Booking>> Create([FromBody] BookingCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Booking()
                    {
                        Start = model.Start,
                        End = model.End,
                        FieldId = model.FieldId,
                        Status = BookingStatusConstant.Wait,
                        UserId = UserId,
                        DateTime = DateTime.Now,
                        Description = model.Description,
                    };

                    var check = _bookingService.CheckBooked(model.FieldId, model.Start, model.End, null);
                    if (check)
                    {
                        return DataResponse<Booking>.False("Field already booked");
                    }


                    var field = _fieldService.GetById(entity.FieldId);
                    if (field != null)
                    {
                        entity.Price = field.Price;
                    }

                    await _bookingService.Create(entity);

                    if (model.Services != null)
                    {
                        foreach (var item in model.Services)
                        {
                            var service = _fieldServiceFeeService.GetById(item);
                            if (service != null)
                            {
                                var servicePay = new ServiceFeePayment()
                                {
                                    BookingId = entity.Id,
                                    Price = service.Price,
                                    FieldServiceFeeId = service.Id,
                                    DateTime = DateTime.Now,
                                };
                                await _serviceFeePaymentService.Create(servicePay);
                            }

                        }
                    }

                    return new DataResponse<Booking>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Booking>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Booking>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }


        [HttpPost("Edit")]
        public async Task<DataResponse<Booking>> Edit([FromBody] BookingEditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new Booking()
                    {
                        Start = model.Start,
                        End = model.End,
                        FieldId = model.FieldId,
                        Status = BookingStatusConstant.Wait,
                        UserId = UserId,
                        DateTime = DateTime.Now,
                        Description = model.Description,
                    };

                    var check = _bookingService.CheckBooked(model.FieldId, model.Start, model.End, entity.Id);
                    if (check)
                    {
                        return DataResponse<Booking>.False("Field already booked");
                    }


                    var field = _fieldService.GetById(entity.FieldId);
                    if (field != null)
                    {
                        entity.Price = field.Price;
                    }

                    await _bookingService.Update(entity);
                    var services = _serviceFeePaymentService.GetQueryable().Where(x => x.BookingId == entity.Id).ToList();
                    await _serviceFeePaymentService.Delete(services);

                    if (model.Services != null)
                    {
                        foreach (var item in model.Services)
                        {
                            var service = _fieldServiceFeeService.GetById(item);
                            if (service != null)
                            {
                                var servicePay = new ServiceFeePayment()
                                {
                                    BookingId = entity.Id,
                                    Price = service.Price,
                                    FieldServiceFeeId = service.Id,
                                    DateTime = DateTime.Now,
                                };
                                await _serviceFeePaymentService.Create(servicePay);
                            }

                        }
                    }

                    return new DataResponse<Booking>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Booking>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Booking>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }


        [HttpPost("UpdateStatus")]
        public async Task<DataResponse<Booking>> UpdateStatus([FromBody] UpdateStatusVM model)
        {
            try
            {
                var entity = _bookingService.GetById(model.Id);
                if (entity != null)
                {
                    entity.Status = model.Status;
                    await _bookingService.Update(entity);
                    return DataResponse<Booking>.True(entity);
                }
                return DataResponse<Booking>.False("Booking not found");

            }
            catch (Exception ex)
            {

                return DataResponse<Booking>.False(ex.Message);
            }

        }


        [HttpGet("GetBooking/{id}")]
        public async Task<DataResponse<BookingDto>> GetBooking(Guid id)
        {
            return await _bookingService.GetDto(id);
        }

        [HttpPost("History")]
        public async Task<DataResponse<PagedList<BookingDto>>> History(BookingSearch search)
        {
            search.UserId = UserId;
            return await _bookingService.History(search);
        }


        [HttpPost("GetBookingActive")]
        public async Task<DataResponse<List<BookingDto>>> GetBookingActive()
        {
           
            return await _bookingService.GetBookingActive(UserId);
        }


        [HttpPost("GetData")]
        public async Task<DataResponse<PagedList<BookingDto>>> GetData(BookingSearch search)
        {
            return await _bookingService.History(search);
        }


        [HttpGet("Payment")]
        public async Task<DataResponse<string>> Payment(Guid id, string returnUrl)
        {
            var url = await _paymentService.CreatePaymentUrl(id, returnUrl, "127.0.0.1");
            if (string.IsNullOrEmpty(url))
            {
                return DataResponse<string>.False("Error");
            }
            return DataResponse<string>.True(url);
        }

        [HttpGet("PaySuccess/{id}")]
        public async Task<DataResponse<Booking>> PaySuccess(Guid id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null)
                return DataResponse<Booking>.False("Booking not found");
            booking.Status = BookingStatusConstant.Paid;
            booking.Paid = true;
            await _bookingService.Update(booking);
            return DataResponse<Booking>.True(booking);
        }


        [HttpGet("Deposit")]
        public async Task<DataResponse<string>> Deposit(Guid id, string returnUrl)
        {
            var url = await _paymentService.CreateDepositUrl(id, returnUrl, "127.0.0.1");
            if (string.IsNullOrEmpty(url))
            {
                return DataResponse<string>.False("Error");
            }
            return DataResponse<string>.True(url);
        }

        [HttpGet("DepositSuccess/{id}")]
        public async Task<DataResponse<Booking>> DepositSuccess(Guid id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null)
                return DataResponse<Booking>.False("Booking not found");
            booking.Deposited = true;
            await _bookingService.Update(booking);
            return DataResponse<Booking>.True(booking);
        }

    }
}