using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Resquest_CertificateOfType
    {
        [Required(ErrorMessage = "phải nhập tên loại")]
        public string Name { get; set; }
        [Required(ErrorMessage = "phải id của loại")]
        public int CertificateId { get; set; }
    }
}
