using Microsoft.Extensions.Caching.Distributed;
using N.Model.Entities;
using N.Repository.BookingRepository;
using N.Repository.ServiceFeeRepository;
using N.Repository.NDirectoryRepository;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Dto;
using N.Service.FieldService.Dto;
using N.Repository.FieldServiceFeeRepository;
using N.Service.FieldServiceFeeService.Dto;
using N.Repository.FieldAreaRepository;

namespace N.Service.FieladService
{
    public class FieldService : Service<Field>, IFieldService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceFeeRepository _serviceFeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFieldAreaRepository _fieldAreaRepository;
        private readonly IFieldServiceFeeRepository _fieldServiceFeeRepository;
        private readonly IDistributedCache _cache;
        public FieldService(
            IFieldRepository fieldRepository,
            IBookingRepository bookingRepository,
            IServiceFeeRepository serviceFeeRepository,
            IUserRepository userRepository,
            IFieldAreaRepository fieldAreaRepository,
            IFieldServiceFeeRepository fieldServiceFeeRepository,
            IDistributedCache cache
            ) : base(fieldRepository)
        {
            this._bookingRepository = bookingRepository;
            this._serviceFeeRepository = serviceFeeRepository;
            this._userRepository = userRepository;
            this._fieldAreaRepository = fieldAreaRepository;
            this._fieldServiceFeeRepository = fieldServiceFeeRepository;
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
                                FieldAreaId = q.FieldAreaId,
                                CreatedDate = q.CreatedDate,
                                Reason = q.Reason,
                            };

                if (search.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == search.UserId);
                }

                if (search.FieldAreaId.HasValue)
                {
                    query = query.Where(x => x.FieldAreaId == search.FieldAreaId);
                }
                if (search.StaffId.HasValue)
                {
                    //query = query.Where(x => x.StaffId == search.StaffId);
                    var staff = _userRepository.GetById(search.StaffId.Value);
                    if (staff != null)
                    {
                        var areaIds = (staff.AreaIds?.Split(",") ?? new string[0]).Select(x => (Guid?)Guid.Parse(x)).ToList();
                        query = query.Where(x => areaIds != null && areaIds.Contains(x.FieldAreaId));
                    }
                    else
                    {
                        query = query.Where(x => false);
                    }

                }
                if (!string.IsNullOrEmpty(search.Status))
                {
                    query = query.Where(x => x.Status == search.Status);
                }

                query = query.OrderByDescending(x => x.CreatedDate);

                var result = PagedList<FieldDto>.Create(query, search);
                foreach (var item in result.Items)
                {
                    item.Services = _fieldServiceFeeRepository.GetQueryable().Where(x => x.FieldId == item.Id).Select(x => new FieldServiceFeeDto()
                    {
                        Id = x.Id,
                        FieldId = x.FieldId,
                        Price = x.Price,
                        ServiceFeeId = x.ServiceFeeId,
                    }).ToList();
                    if (item.Services.Any())
                    {
                        foreach (var service in item.Services)
                        {
                            service.ServiceName = _serviceFeeRepository.GetQueryable().Where(x => x.Id == service.ServiceFeeId).FirstOrDefault()?.Name;
                        }
                    }
                }
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

        public DataResponse<FieldDto> GetDto(Guid id, int? ngay, int? thang, int? nam)
        {
            try
            {
                var query = (from q in GetQueryable()
                            .Where(x => x.Id == id)
                             join service in _fieldServiceFeeRepository.GetQueryable()
                             on q.Id equals service.FieldId
                             into gService
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
                                 FieldAreaId = q.FieldAreaId,
                                 Reason = q.Reason,
                             }).FirstOrDefault();

                if (query != null)
                {
                    var fieldTimes = GetFieldTimes(new FieldTimeSearch() { FieldId = query.Id, Ngay = ngay, Thang = thang, Nam = nam });
                    query.FieldTimes = fieldTimes.Data;

                    query.Services = _fieldServiceFeeRepository.GetQueryable().Where(x => x.FieldId == query.Id).Select(x => new FieldServiceFeeDto()
                    {
                        Id = x.Id,
                        FieldId = x.FieldId,
                        Price = x.Price,
                        ServiceFeeId = x.ServiceFeeId,
                    }).ToList();
                    if (query.Services.Any())
                    {
                        foreach (var service in query.Services)
                        {
                            service.ServiceName = _serviceFeeRepository.GetQueryable().Where(x => x.Id == service.ServiceFeeId).FirstOrDefault()?.Name;
                        }
                    }
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

        //public DataResponse<List<ServiceFee>> GetServiceFees(Guid id)
        //{
        //    var serviceFees = _serviceFeeRepository.GetQueryable().Where(x => x.FieldId == id).ToList();
        //    return new DataResponse<List<ServiceFee>>()
        //    {
        //        Data = serviceFees,
        //        Success = true,
        //    };
        //}

        public DataResponse<List<FieldTime>> GetFieldTimes(FieldTimeSearch search)
        {
            try
            {
                var result = new List<FieldTime>();
                var start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 0, 0);
                var end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 0, 0);


                if (search != null)
                {
                    if (search.Ngay.HasValue && search.Thang.HasValue && search.Nam.HasValue)
                    {
                        start = new DateTime(search.Nam.Value, search.Thang.Value, search.Ngay.Value, 5, 0, 0);
                        end = new DateTime(search.Nam.Value, search.Thang.Value, search.Ngay.Value, 23, 0, 0);
                    }

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
                            end = end.AddHours(1.5);
                        }
                    }
                }

                var bookings = _bookingRepository.GetQueryable().Where(x => x.Start >= start && x.End <= end
                                && (search == null || !search.FieldId.HasValue || x.FieldId == search.FieldId)).ToList();

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
                        if (bookings.Any(x => x.Start >= item.Start && x.End <= item.End))
                        {
                            item.Booked = true;
                        }
                        if (item.Start > DateTime.Now)
                        {
                            item.Expired = true;
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
