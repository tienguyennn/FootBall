using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels;
using N.Model.Entities;
using N.Service.BookingService;
using N.Service.BookingService.Dto;
using N.Service.Common;
using N.Service.DTO;
using N.Service.FieladService;
using N.Service.FieldService.Dto;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class BookingController : NController
    {
        private readonly IBookingService _bookingService;
        private readonly IFieldService _fieldService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingController> _logger;


        public BookingController(
            IBookingService bookingService,
            IFieldService fieldService,
            IMapper mapper,
            ILogger<BookingController> logger
            )
        {
            this._bookingService = bookingService;
            this._fieldService = fieldService;
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
                        Status = model.Status,
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
                    return new DataResponse<Booking>() { Data = entity, Success = true };
                }
                catch (Exception ex)
                {
                    DataResponse<Booking>.False("Error", new string[] { ex.Message });
                }
            }
            return DataResponse<Booking>.False("Some properties are not valid", ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)));
        }


        [HttpGet("GetBooking/{id}")]
        public DataResponse<BookingDto> GetBooking(Guid id)
        {
            return _bookingService.GetDto(id);
        }

        [HttpPost("History")]
        public DataResponse<PagedList<BookingDto>> History(BookingSearch search)
        {
            return _bookingService.History(search);
        }

    }
}