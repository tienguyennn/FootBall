using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

    }
}