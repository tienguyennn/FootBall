using Google.Apis.Drive.v3.Data;
using N.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N.Service.Dto
{
    public class AppUserDto
    {
        public Guid? Id { get; set; }
        public Guid? StaffId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Picture { get; set; }
        public List<Guid>? AreaIds { get; set; }
        public List<FieldArea>? Areas { get; set; }
        public string? Type { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }

        public static AppUserDto FromAppUser(AppUser user)
        {
            return new AppUserDto()
            {
                StaffId = user.StaffId,
                Gender = user.Gender,
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Picture = user.Picture,
                Type = user.Type,
                Phone = user.PhoneNumber,
                AreaIds = user.AreaIds?.Split(",")?.Select(x=> Guid.Parse(x)).ToList()
            };
        }
    }
}
