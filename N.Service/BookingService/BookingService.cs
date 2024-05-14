using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using N.Model.Entities;
using N.Repository.BookingRepository;
using N.Repository.NDirectoryRepository;
using N.Repository.ServiceFeePaymentRepository;
using N.Service.BookingService.Dto;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Dto;
using N.Service.FieladService;
using N.Service.FieldService.Dto;
using N.Service.FieldServiceFeeService;
using N.Service.ServiceFeePaymentService.Dto;

namespace N.Service.BookingService
{
    public class BookingService : Service<Booking>, IBookingService
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IServiceFeePaymentRepository _serviceFeePaymentRepository;
        private readonly IFieldServiceFeeService _fieldServiceFeeService;
        private readonly IFieldService _fieldService;
        private readonly IUserRepository _userRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IFieldRepository fieldRepository,
            IServiceFeePaymentRepository serviceFeePaymentRepository,
            IFieldServiceFeeService fieldServiceFeeService,
            IFieldService fieldService,
            IUserRepository userRepository
            ) : base(bookingRepository)
        {
            this._fieldRepository = fieldRepository;
            this._serviceFeePaymentRepository = serviceFeePaymentRepository;
            this._fieldServiceFeeService = fieldServiceFeeService;
            this._fieldService = fieldService;
            this._userRepository = userRepository;
        }

        public bool CheckBooked(Guid? fieldId, DateTime? start, DateTime? end, Guid? id = null)
        {

            var booked = GetQueryable().Where(x => x.Start >= start && x.End <= end
                             && x.FieldId == fieldId && x.Id != id).Any();
            return booked;
        }

        public async Task<DataResponse<List<BookingDto>>> GetBookingActive(Guid? userId)
        {
            var result = await (from q in GetQueryable().Where(x => x.Start >= DateTime.Now)
                                join user in _userRepository.GetQueryable().Where(x => x.Id == userId)
                                on q.UserId equals user.Id
                                join field in _fieldRepository.GetQueryable()
                                on q.FieldId equals field.Id
                                select new BookingDto()
                                {
                                    Description = q.Description,
                                    Id = q.Id,
                                    UserId = q.UserId,
                                    Price = q.Price,
                                    Status = q.Status,
                                    DateTime = q.DateTime,
                                    End = q.End,
                                    FieldId = q.FieldId,
                                    Start = q.Start,
                                    Paid = q.Paid,
                                    Deposited = q.Deposited,
                                    CreatedDate = q.CreatedDate,
                                    User = AppUserDto.FromAppUser(user),
                                    Field = field,
                                }).ToListAsync();

            foreach (var item in result)
            {
                var payments = _serviceFeePaymentRepository.GetQueryable()
                    .Where(x => x.BookingId == item.Id).Select(x => new ServiceFeePaymentDto()
                    {
                        Id = x.Id,
                        BookingId = item.Id,
                        DateTime = x.DateTime,
                        Description = x.Description,
                        FieldServiceFeeId = x.FieldServiceFeeId,
                        Price = x.Price,
                    }).ToList();
                foreach (var payment in payments)
                {
                    payment.FieldService = (await _fieldServiceFeeService.GetDto(payment.FieldServiceFeeId ?? Guid.Empty)).Data;
                }
                item.Services = payments;
                if (item.Services != null && item.Services.Any())
                {
                    item.Price = item.Price ?? 0;
                    foreach (var service in item.Services)
                    {
                        item.Price += service.Price ?? 0;
                    }
                }
            }

            return DataResponse<List<BookingDto>>.True(result);
        }

        public async Task<DataResponse<BookingDto>> GetDto(Guid id)
        {
            var query = await (from q in GetQueryable().Where(x => x.Id == id)
                               join user in _userRepository.GetQueryable()
                               on q.UserId equals user.Id
                               join field in _fieldRepository.GetQueryable()
                               on q.FieldId equals field.Id
                               select new BookingDto()
                               {
                                   Description = q.Description,
                                   Id = q.Id,
                                   UserId = q.UserId,
                                   Price = q.Price,
                                   Status = q.Status,
                                   DateTime = q.DateTime,
                                   End = q.End,
                                   FieldId = q.FieldId,
                                   Paid = q.Paid,
                                   Deposited = q.Deposited,
                                   CreatedDate = q.CreatedDate,
                                   Start = q.Start,
                                   User = AppUserDto.FromAppUser(user),
                                   Field = field,
                               }).FirstOrDefaultAsync();
            if (query != null)
            {

                var payments = _serviceFeePaymentRepository.GetQueryable()
                       .Where(x => x.BookingId == query.Id).Select(x => new ServiceFeePaymentDto()
                       {
                           Id = x.Id,
                           BookingId = query.Id,
                           DateTime = x.DateTime,
                           Description = x.Description,
                           FieldServiceFeeId = x.FieldServiceFeeId,
                           Price = x.Price,
                       }).ToList();
                foreach (var payment in payments)
                {
                    payment.FieldService = (await _fieldServiceFeeService.GetDto(payment.FieldServiceFeeId ?? Guid.Empty)).Data;
                }
                query.Services = payments;

                if (query.Services != null && query.Services.Any())
                {
                    query.Price = query.Price ?? 0;
                    foreach (var item in query.Services)
                    {
                        query.Price += item.Price ?? 0;
                    }
                }
            }

            return new DataResponse<BookingDto>()
            {
                Data = query,
                Success = true,
            };
        }

        public async Task<DataResponse<PagedList<BookingDto>>> History(BookingSearch search)
        {
            var query = from q in GetQueryable()
                        join user in _userRepository.GetQueryable()
                        on q.UserId equals user.Id
                        join field in _fieldRepository.GetQueryable()
                        on q.FieldId equals field.Id
                        select new BookingDto()
                        {
                            Description = q.Description,
                            Id = q.Id,
                            UserId = q.UserId,
                            Price = q.Price,
                            Status = q.Status,
                            DateTime = q.DateTime,
                            End = q.End,
                            FieldId = q.FieldId,
                            Start = q.Start,
                            Paid = q.Paid,
                            Deposited = q.Deposited,
                            CreatedDate = q.CreatedDate,
                            User = AppUserDto.FromAppUser(user),
                            Field = field,
                        };

            if (search.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }
            if (search.FieldId.HasValue)
            {
                query = query.Where(x => x.FieldId == search.FieldId);
            }
            if (search.OwnerId.HasValue)
            {
                query = query.Where(x => x.Field != null && x.Field.UserId == search.OwnerId);
            }
            if (search.Start.HasValue)
            {
                query = query.Where(x => x.Start >= search.Start);
            }
            if (search.End.HasValue)
            {
                query = query.Where(x => x.End <= search.End);
            }
            if (search.Deposited.HasValue)
            {
                query = query.Where(x => x.Deposited == search.Deposited);
            }
            if (search.Paid.HasValue)
            {
                query = query.Where(x => x.Paid == search.Paid);
            }
            query = query.OrderByDescending(x => x.CreatedDate);
            var result = PagedList<BookingDto>.Create(query, search);
            foreach (var item in result.Items)
            {
                var payments = _serviceFeePaymentRepository.GetQueryable()
                    .Where(x => x.BookingId == item.Id).Select(x => new ServiceFeePaymentDto()
                    {
                        Id = x.Id,
                        BookingId = item.Id,
                        DateTime = x.DateTime,
                        Description = x.Description,
                        FieldServiceFeeId = x.FieldServiceFeeId,
                        Price = x.Price,
                    }).ToList();
                foreach (var payment in payments)
                {
                    payment.FieldService = (await _fieldServiceFeeService.GetDto(payment.FieldServiceFeeId ?? Guid.Empty)).Data;
                }
                item.Services = payments;
                if (item.Services != null && item.Services.Any())
                {
                    item.Price = item.Price ?? 0;
                    foreach (var service in item.Services)
                    {
                        item.Price += service.Price ?? 0;
                    }
                }
            }
            return new DataResponse<PagedList<BookingDto>>()
            {
                Data = result,
                Success = true,
            };
        }
    }
}
