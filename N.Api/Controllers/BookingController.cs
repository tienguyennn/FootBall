using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.BookingService;
using N.Service.DTO;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : NController
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingController> _logger;


        public BookingController(
            IBookingService bookingService,
            IMapper mapper,
            ILogger<BookingController> logger
            )
        {
            this._bookingService = bookingService;
            this._mapper = mapper;
            _logger = logger;
        }
        [HttpPost("Create")]
        public DataResponse<Booking> Create([FromBody] BookingCreateVM model)
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
                        Status = model.Status,
                        UserId = UserId,
                        Description = model.Description,
                    };
                    _bookingService.Create(entity);
                    return new DataResponse<Booking>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Booking>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Booking>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }

    }
}