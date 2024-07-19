using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DemoBTL.Domain.Entity.Users.Function
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
