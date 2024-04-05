using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace N.Controllers
{
    [ApiController]
    [Authorize]
    public class NController : ControllerBase
    {
        public NController() { }

        internal Guid? UserId
        {
            get
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(id, out var userId))
                {
                    return userId;
                }
                return null;
            }
        }

        internal string GetUri()
        {
            var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            return uriBuilder.Uri.AbsoluteUri;
        }
        internal string GetIp()
        {
            var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host);
            return uriBuilder.Uri.AbsoluteUri;
        }

        internal IEnumerable<string> ModelStateError
                => ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage));

    }


}