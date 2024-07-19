using DemoBTL.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoBTL.Domain.InterfacReponsitories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);

        Task AddrollToUserAsync(User user, List<string> ListRole);
        Task<IEnumerable<string>> GetRoleOfUserAsync(User user);
        Task DeleteRoleAsync(User user, List<string> roles);  
    }
}
