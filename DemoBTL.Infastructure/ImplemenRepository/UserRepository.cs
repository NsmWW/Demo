using DemoBTL.Domain.Entity;
using DemoBTL.Domain.Entity.StutyStudent.Course.Detail;
using DemoBTL.Domain.Entity.Users.Function;
using DemoBTL.Domain.InterfacReponsitories;
using DemoBTL.Infastructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoBTL.Infastructure.ImplemenRepository
{
    public class UserRepository :IUserRepository
    {
        private readonly ApplicationDBcontext _context;
        public UserRepository(ApplicationDBcontext context) 
        {
            _context = context;
        }
        public UserRepository() { }


        // Phần này so sánh chuỗi để xử lý phần thêm Role cho User
        #region
        private Task<bool> CompareString(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(), str2.ToLowerInvariant()));
        }
        private async Task<bool> IsStringInListAsync(string st1, List<string> ListString)
        {
            if (st1 == null)
            {
                throw new ArgumentNullException("chuôi đầu nhập vào không có gì");
            }
            if (ListString == null)
            {
                throw new ArgumentNullException("List đầu nhập vào không có gì");
            }
            foreach (string item in ListString)
            {
                if (await CompareString(st1, item))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        //Phần này là thêm List role cho user(cái đầu), cái sau lấy tất cả Role của user theo id để tiến hành so sánh
        #region
        public async Task AddrollToUserAsync(User user, List<string> ListRole)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User không có gì");
            }
            if (ListRole == null)
            {
                throw new ArgumentNullException("ListRole không có gì");
            }
            foreach (var role in ListRole.Distinct())
            {
                var roleOfUser = await GetRoleOfUserAsync(user);
                if (await IsStringInListAsync(role, roleOfUser.ToList()))
                {
                    throw new ArgumentNullException("người dùng đã có quyền này");
                }
                else
                {
                    var roleitem = await _context.Roles.SingleOrDefaultAsync(x => x.RoleCode.Equals(role));
                    if (roleitem == null)
                    {
                        throw new ArgumentNullException("không có quyền này cần phải thêm list quyền");
                    }
                    _context.Permissions.Add(new Permission
                    {
                        RoleId = roleitem.Id,
                        UserId = user.Id,
                    });
                }
                _context.SaveChanges();
            }
        }
        public async Task<IEnumerable<string>> GetRoleOfUserAsync(User user)
        {
            var roles = new List<string>();
            var ListRole = _context.Permissions.Where(x => x.UserId == user.Id).AsQueryable();
            foreach (var item in ListRole.Distinct())
            {
                var role = _context.Roles.SingleOrDefault(x => x.Id == item.RoleId);
                roles.Add(role.RoleCode);
            }
            return roles.AsEnumerable();
        }
        #endregion

        public async Task<User> GetUserByEmail(string email)
        {
            var user =await _context.User.SingleOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.User.SingleOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            return user;
        }

        public async Task DeleteRoleAsync(User user, List<string> roles)
        {
            var listrole = await GetRoleOfUserAsync(user);
            if (listrole == null)
            {
                throw new AbandonedMutexException("khong lay duoc list role cua user trong delete role");
            }
            if (roles == null)
            {
                throw new AbandonedMutexException("list role truyen vao khong co gi");
            }
            foreach (var role in listrole)
            {
                foreach (var item in roles)
                {
                    var roleoject = _context.Roles.SingleOrDefault(x => x.RoleCode.Equals(item));
                    var permission = _context.Permissions.SingleOrDefault(x => x.RoleId == roleoject.Id && x.UserId == user.Id);
                    if (await CompareString(role, item))
                    {
                        _context.Permissions.Remove(permission);
                    }
                }
            }
            _context.SaveChanges();
        }
    }
}
