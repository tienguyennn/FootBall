using Microsoft.AspNetCore.Identity;

namespace N.Api.ViewModels
{
    public class AcceptVM
    {
        public Guid? Id { get; set; }
        public bool? Accept { get; set; }
    }
}