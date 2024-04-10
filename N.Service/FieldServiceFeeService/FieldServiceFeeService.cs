using N.Model.Entities;
using N.Repository.FieldServiceFeeRepository;
using N.Service.Common.Service;
using N.Service.FieldServiceFeeService.Dto;
using N.Service.Common;
using N.Service.Dto;
using Microsoft.EntityFrameworkCore;

namespace N.Service.FieldServiceFeeService
{
    public class FieldServiceFeeService : Service<FieldServiceFee>, IFieldServiceFeeService
    {
        public FieldServiceFeeService(
            IFieldServiceFeeRepository fieldServiceFeeRepository
            ) : base(fieldServiceFeeRepository)
        {
        }

        public async Task<DataResponse<PagedList<FieldServiceFeeDto>>> GetData(FieldServiceFeeSearch search)
        {
            try
            {
                var query = from q in GetQueryable()
                            select new FieldServiceFeeDto()
                            {
                                Id = q.Id,
                                CreatedDate = q.CreatedDate,
                            };

                query = query.OrderByDescending(x => x.CreatedDate);
                var result = await PagedList<FieldServiceFeeDto>.CreateAsync(query, search);
                return new DataResponse<PagedList<FieldServiceFeeDto>>()
                {
                    Data = result,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<PagedList<FieldServiceFeeDto>>.False(ex.Message);
            }

        }

        public async Task<DataResponse<FieldServiceFeeDto>> GetDto(Guid id)
        {
            try
            {
                var item = await (from q in GetQueryable()
                            select new FieldServiceFeeDto()
                            {
                                Id = q.Id,
                            }).FirstOrDefaultAsync();

                return new DataResponse<FieldServiceFeeDto>()
                {
                    Success = true,
                    Data = item,
                };

            }
            catch (Exception ex)
            {
                return DataResponse<FieldServiceFeeDto>.False(ex.Message);
            }
        }

    }
}
