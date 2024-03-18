using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.DTO;
using N.Service.FeePaymentService;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class FeePaymentController : NController
    {
        private readonly IFeePaymentService _feePaymentService;
        private readonly IMapper _mapper;
        private readonly ILogger<FeePaymentController> _logger;


        public FeePaymentController(
            IFeePaymentService feePaymentService,
            IMapper mapper,
            ILogger<FeePaymentController> logger
            )
        {
            this._feePaymentService = feePaymentService;
            this._mapper = mapper;
            _logger = logger;
        }


        [HttpPost("Create")]
        public async Task<DataResponse<FeePayment>> Create([FromBody] FeePaymentCreateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = new FeePayment()
                    {
                        BookingId = model.BookingId,
                        FeeId = model.FeeId,
                        DateTime = model.DateTime,
                        Price = model.Price,
                        Description = model.Description,
                    };
                    await _feePaymentService.Create(entity);
                    return new DataResponse<FeePayment>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<FeePayment>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<FeePayment>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }

    }
}