using DemoBTL.Domain.Entity.Address.W.D.P;
using DemoBTL.Domain.Entity.Cerificate.Detail;
using DemoBTL.Domain.Entity.Enumerates;
using DemoBTL.Domain.Entity.StutyStudent;
using DemoBTL.Domain.Entity.StutyStudent.Answers.Question;
using DemoBTL.Domain.Entity.Users.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Domain.Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? Avatar { get; set; }
        public string Email { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = false;
        public ConstanEnum.EnumStatusUser UserStatus { get; set; } = ConstanEnum.EnumStatusUser.Unactive;

        // kết nối với địa chỉ 
        public int? WardId { get; set; }
        public virtual Ward? Ward { get; set; }
        public int? DistrictId { get; set; }
        public virtual District? District { get; set; }
        public int? ProvinceId { get; set; }
        public virtual Province? Province { get; set; }

        //chứng chỉ 
        public virtual ICollection<Certificate>? Cerificates { get; set; }

        // Quyền 
        public virtual ICollection<Permission>? Permissions { get; set; }


        //Thông báo 
        public int? NotificationId { get; set; }
        public virtual Notification? Notification { get; set; }


        // đăng ký khoá 1 hs = nhiều khoá 
        public virtual ICollection<RegisterStudy>? RegisterStudies { get; set; }

        public virtual ICollection<Answers>? Answers { get; set; }

    }
}
