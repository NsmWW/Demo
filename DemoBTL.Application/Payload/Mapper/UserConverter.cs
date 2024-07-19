using DemoBTL.Application.Payload.ResponeModels.DataUser;
using DemoBTL.Domain.Entity;

namespace DemoBTL.Application.Payload.Mapper
{
    public class UserConverter
    {
        public DataResponeUser EntityToDTO(User user)
        {
            return new DataResponeUser
            {
                Fullname = user.Username,
                DateofBirth = user.DateofBirth,
                Email = user.Email,
                Id = user.Id,
                UserStatus = user.UserStatus.ToString(),
            };
        }
    }
}
