using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Service.FeeService;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class FeeController : NController
    {
        private readonly IFeeService _feeService;
        private readonly IMapper _mapper;
        private readonly ILogger<FeeController> _logger;


        public FeeController(
            IFeeService feeService,
            IMapper mapper,
            ILogger<FeeController> logger
            )
        {
            this._feeService = feeService;
            this._mapper = mapper;
            _logger = logger;
        }

    }
}