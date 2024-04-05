using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.Common
{
    public class UploadFile
    {
        public static async Task<UploadFileResult> Save(IFormFile file)
        {
            var result = new UploadFileResult();
            try
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Root");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                result.Success = true;
                result.Path = fileName;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;


        }
    }

    public class UploadFileResult
    {
        public bool Success { get; set; }
        public string? Path { get; set; }
        public string? Error { get; set; }
    }
}
