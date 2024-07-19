using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Request_Subject
    {
        [Required(ErrorMessage ="Tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cái này sẽ phải là ảnh")]
        public string Symbol { get; set; }
    }
}
