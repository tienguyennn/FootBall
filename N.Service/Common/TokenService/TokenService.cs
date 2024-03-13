using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using N.Extensions;

namespace N.Service.Common.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(IDistributedCache cache, IHttpContextAccessor httpContextAccessor)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<bool> IsActiveAsync(string? token)
        {
            return await _cache.GetStringAsync(GetKey(token)) == null;
        }
        public async Task DeactivateAsync(string token)
             => await _cache.SetStringAsync(GetKey(token), "", new DistributedCacheEntryOptions
             {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(AppSettings.AuthSettings.SecondsExpires)
             });
        public async Task<bool> IsCurrentActiveToken() => await IsActiveAsync(GetCurrentToken());
        public async Task DeactivateCurrentToken()
            => await DeactivateAsync(GetCurrentToken());

        private string? GetCurrentToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["authorization"];
            if (authorizationHeader.HasValue && authorizationHeader.Value != StringValues.Empty)
                return authorizationHeader.Value.Single()?.Split(" ").LastOrDefault();
            return null;
        }

        private string GetKey(string? token)
            => $"tokens:{token}:deactivated";



        public async Task<string?> GenerateQrLogin(string guid, string userId)
        {
            await _cache.SetStringAsync(guid, userId, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(AppSettings.AuthSettings.SecondsExpires)
            });
            return guid;
        }

        public async Task<string?> GetQrLogin(string guid)
        {
            return await _cache.GetStringAsync(guid);
        }
    }
}
