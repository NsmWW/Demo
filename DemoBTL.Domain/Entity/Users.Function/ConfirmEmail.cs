using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DemoBTL.Domain.Entity.Users.Function
{
    public class ConfirmEmail : BaseEntity
    {
        public string ConfirmCode { get; set; }
        public DateTime Expirytime { get; set; }
        public bool IsConfirm { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
