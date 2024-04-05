using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using N.Extensions;

namespace N.Service.Common.RequestFileService
{
    public class RequestFileService : IRequestFileService
    {
        private readonly IDistributedCache _cache;

        public RequestFileService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string?> AccessFile(string? guid)
            => await _cache.GetStringAsync(guid);


        public async Task<string> RequestFile(string? path)
        {
            var guid = Guid.NewGuid().ToString();
            await _cache.SetStringAsync(guid, path, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(AppSettings.SecondsRequestFile)
            });
            return guid;
        }
    }
}
