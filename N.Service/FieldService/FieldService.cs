using Microsoft.Extensions.Caching.Distributed;
using N.Model.Entities;
using N.Repository.BookingRepository;
using N.Repository.FeeRepository;
using N.Repository.NDirectoryRepository;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.DTO;
using N.Service.FieldService.Dto;

namespace N.Service.FieladService
{
    public class FieldService : Service<Field>, IFieldService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFeeRepository _feeRepository;
        private readonly IDistributedCache _cache;
        public FieldService(
            IFieldRepository fieldRepository,
            IBookingRepository bookingRepository,
            IFeeRepository feeRepository,
            IDistributedCache cache
            ) : base(fieldRepository)
        {
            this._bookingRepository = bookingRepository;
            this._feeRepository = feeRepository;
            this._cache = cache;
        }

        public DataResponse<PagedList<FieldDto>> GetData(FieldSearch search)
        {
            try
            {
                var query = from q in GetQueryable()
                            select new FieldDto()
                            {
                                Address = q.Address,
                                Description = q.Description,
                                Id = q.Id,
                                Name = q.Name,
                                Picture = q.Picture,
                                UserId = q.UserId,
                                Price = q.Price,
                                StaffId = q.StaffId,
                                Status = q.Status,
                            };


                if (search.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == search.UserId);
                }
                if (search.StaffId.HasValue)
                {
                    query = query.Where(x => x.StaffId == search.StaffId);
                }
                if (!string.IsNullOrEmpty(search.Status))
                {
                    query = query.Where(x => x.Status == search.Status);
                }

                var result = PagedList<FieldDto>.Create(query, search);
                return new DataResponse<PagedList<FieldDto>>()
                {
                    Data = result,
                    Success = true,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<PagedList<FieldDto>>.False(ex.Message);
            }

        }

        public DataResponse<FieldDto> GetDto(Guid id)
        {
            try
            {
                var query = (from q in GetQueryable()
                            .Where(x=>x.Id == id)
                            select new FieldDto()
                            {
                                Address = q.Address,
                                Description = q.Description,
                                Id = q.Id,
                                Name = q.Name,
                                Picture = q.Picture,
                                UserId = q.UserId,
                                Price = q.Price,
                                StaffId = q.StaffId,
                                Status = q.Status,
                            }).FirstOrDefault();

                if(query != null)
                {
                    var fieldTimes = GetFieldTimes(new FieldTimeSearch() { FieldId = query.Id });
                    query.FieldTimes = fieldTimes.Data;
                }



                return new DataResponse<FieldDto>()
                {
                    Data = query,
                    Success = true,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<FieldDto>.False(ex.Message);
            }
        }

        public DataResponse<List<Fee>> GetFees(Guid id)
        {
            var fees = _feeRepository.GetQueryable().Where(x => x.FieldId == id).ToList();
            return new DataResponse<List<Fee>>()
            {
                Data = fees,
                Success = true,
            };
        }

        public DataResponse<List<FieldTime>> GetFieldTimes(FieldTimeSearch search)
        {
            try
            {
                var result = new List<FieldTime>();
                var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0, 0);
                var end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);

                if (search != null)
                {
                    if (search.Start.HasValue)
                    {
                        while (start > search.Start)
                        {
                            start = start.AddHours(-1.5);
                        }
                    }
                    if (search.End.HasValue)
                    {
                        while (end < search.End)
                        {
                            start = start.AddHours(1.5);
                        }
                    }
                }

                var bookings = _bookingRepository.GetQueryable().Where(x => x.Start >= start && x.End <= end 
                                &&(search == null || !search.FieldId.HasValue || x.FieldId == search.FieldId)).ToList();

                while (start < end)
                {
                    if (start.Hour >= 5 && start.Hour < 22)
                    {
                        var item = new FieldTime()
                        {
                            FieldId = search?.FieldId,
                            Start = start,
                            End = start.AddHours(1.5),
                        };
                        if(bookings.Any(x=>x.Start >= item.Start && x.End<= item.End))
                        {
                            item.Booked = true;
                        }
                        result.Add(item);
                    }
                    start = start.AddHours(1.5);
                }
                return new DataResponse<List<FieldTime>>()
                {
                    Data = result,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {

                return DataResponse<List<FieldTime>>.False(ex.Message);
            }

        }

    }
}
