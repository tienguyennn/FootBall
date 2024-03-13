using Microsoft.Extensions.Caching.Distributed;
using N.Model.Entities;
using N.Repository.NDirectoryRepository;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.DTO;
using N.Service.FieldService.Dto;

namespace N.Service.FieladService
{
    public class FieldService : Service<Field>, IFieldService
    {
        private readonly IDistributedCache _cache;
        public FieldService(
            IFieldRepository fieldRepository,
            IDistributedCache cache
            ) : base(fieldRepository)
        {
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
                            };


                if (search.UserId.HasValue)
                {
                    query = query.Where(x => x.UserId == search.UserId);
                }

                var result = PagedList<FieldDto>.Create(query, search.PageIndex, search.PageSize);

                return new DataResponse<PagedList<FieldDto>>()
                {
                    Data = result,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return DataResponse<PagedList<FieldDto>>.False(ex.Message);
            }

        }
    }
}
