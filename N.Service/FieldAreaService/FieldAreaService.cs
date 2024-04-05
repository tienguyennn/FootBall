using N.Model.Entities;
using N.Repository.FieldAreaRepository;
using N.Service.Common.Service;
using N.Service.FieldAreaService.Dto;
using N.Service.Common;
using N.Service.Dto;

namespace N.Service.FieldAreaService
{
    public class FieldAreaService : Service<FieldArea>, IFieldAreaService
    {
        public FieldAreaService(
            IFieldAreaRepository fieldAreaRepository
            ) : base(fieldAreaRepository)
        {
        }

        public DataResponse<PagedList<FieldAreaDto>> GetData(FieldAreaSearch search)
        {
            try
            {
                var query = from q in GetQueryable()
                            select new FieldAreaDto()
                            {
                                Id = q.Id,
                                Description = q.Description,
                                Name = q.Name,
                            };

                var result = PagedList<FieldAreaDto>.Create(query, search);
                return new DataResponse<PagedList<FieldAreaDto>>()
                {
                    Data = result,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<PagedList<FieldAreaDto>>.False(ex.Message);
            }

        }

        public DataResponse<FieldAreaDto> GetDto(Guid id)
        {
            try
            {
                var query = from q in GetQueryable()
                            select new FieldAreaDto()
                            {
                                Id = q.Id,
                                Description = q.Description,
                                Name = q.Name,
                            };

                var result = query.FirstOrDefault();
                return new DataResponse<FieldAreaDto>()
                {
                    Data = result,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<FieldAreaDto>.False(ex.Message);
            }
        }
    }
}
