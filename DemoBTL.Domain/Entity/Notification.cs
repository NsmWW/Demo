using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DemoBTL.Domain.Entity
{
    public class Notification : BaseEntity
    {
        public string? Image { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public bool IsSeen { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
