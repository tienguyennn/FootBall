using N.Service.Common.RequestFileService;

namespace NDrive.Core.Middleware
{
    public class FileProtectedMiddleware : IMiddleware
    {
        private readonly IRequestFileService _RequestFileService;

        public FileProtectedMiddleware(IRequestFileService RequestFileService)
        {
            this._RequestFileService = RequestFileService;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ip = context.Connection.RemoteIpAddress;
            File.AppendAllText("log-host.txt", ip + "\n");

            var path = context.Request.Path;

            //context.Request.Headers.Add("ngrok-skip-browser-warning", "123");
            if (path.HasValue)
            {
                var pathValue = path.Value.ToLower();

                if (pathValue.StartsWith("/drive/"))
                {
                    var guid = path.Value.Replace("/drive/", "").Split('/', '\\').FirstOrDefault();
                    var access = await _RequestFileService.AccessFile(guid);
                    if (!string.IsNullOrEmpty(access))
                    {
                        context.Request.Path = $"/root/{access}";
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }
                else if (pathValue.StartsWith("/avatar/"))
                {
                    context.Request.Path = "/root" + pathValue;
                }
                else if (pathValue.StartsWith("/mobile/"))
                {
                    // trường hợp truy cập từ mobile

                    //lấy url đã được cấp quyền
                    var urlMobile = path.Value.Replace("/mobile/", "").Split('/', '\\').FirstOrDefault();
                    // kiểm tra xem url có quyền truy cập không
                    var access = await _RequestFileService.AccessFile(urlMobile);
                    if (!string.IsNullOrEmpty(access))
                    {
                        // chuyển hướng url đến file cần truy câpk

                        if (path.Value.StartsWith($"/mobile/{urlMobile}/avatar/"))
                        {
                            context.Request.Path = path.Value.Replace($"/mobile/{urlMobile}/", "/root/");
                        }
                        else
                        {
                            context.Request.Path = path.Value.Replace("/mobile/", "/root/").Replace($"/{urlMobile}/", $"/{access}/");
                        }
                    }
                    else
                    {
                        //nếu không trả về không có quyền
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }
                else if (pathValue.StartsWith("/qrcode/"))
                {
                    context.Request.Path = "/root" + pathValue;
                }
            }

            await next(context);
        }

    }

}
