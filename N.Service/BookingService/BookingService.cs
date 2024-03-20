using N.Model.Entities;
using N.Repository.BookingRepository;
using N.Repository.NDirectoryRepository;
using N.Service.BookingService.Dto;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.DTO;
using N.Service.FieldService.Dto;

namespace N.Service.BookingService
{
    public class BookingService : Service<Booking>, IBookingService
    {
        private readonly IFieldRepository _fieldRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IFieldRepository fieldRepository,
            IUserRepository userRepository
            ) : base(bookingRepository)
        {
            this._fieldRepository = fieldRepository;
            this._userRepository = userRepository;
        }

        public bool CheckBooked(Guid? fieldId, DateTime? start, DateTime? end, Guid? id = null)
        {

            var booked = GetQueryable().Where(x => x.Start >= start && x.End <= end
                             && x.FieldId == fieldId).Any();
            return booked;
        }

        public DataResponse<BookingDto> GetDto(Guid id)
        {
            var query = (from q in GetQueryable()
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
                            User = AppUserDto.FromAppUser(user),
                            Field = field,
                        }).FirstOrDefault();

            return new DataResponse<BookingDto>()
            {
                Data = query,
                Success = true,
            };
        }

        public DataResponse<PagedList<BookingDto>> History(BookingSearch search)
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
            if (search.Start.HasValue)
            {
                query = query.Where(x => x.Start >= search.Start);
            }
            if (search.End.HasValue)
            {
                query = query.Where(x => x.End <= search.End);
            }

            var result = PagedList<BookingDto>.Create(query, search);

            return new DataResponse<PagedList<BookingDto>>()
            {
                Data = result,
                Success = true,
            };
        }
    }
}
