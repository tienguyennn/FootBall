using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.Dto;
using N.Service.ServiceFeePaymentService;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class ServiceFeePaymentController : NController
    {
        private readonly IServiceFeePaymentService _serviceFeePaymentService;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceFeePaymentController> _logger;


        public ServiceFeePaymentController(
            IServiceFeePaymentService serviceFeePaymentService,
            IMapper mapper,
            ILogger<ServiceFeePaymentController> logger
            )
        {
            this._serviceFeePaymentService = serviceFeePaymentService;
            this._mapper = mapper;
            _logger = logger;
        }


        [HttpPost("Create")]
        public async Task<DataResponse<ServiceFeePayment>> Create([FromBody] ServiceFeePaymentCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new ServiceFeePayment()
                    {
                        BookingId = model.BookingId,
                        FieldServiceFeeId = model.FieldServiceFeeId,
                        DateTime = model.DateTime,
                        Price = model.Price,
                        Description = model.Description,
                    };
                    await _serviceFeePaymentService.Create(entity);
                    return new DataResponse<ServiceFeePayment>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<ServiceFeePayment>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<ServiceFeePayment>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }

    }
}