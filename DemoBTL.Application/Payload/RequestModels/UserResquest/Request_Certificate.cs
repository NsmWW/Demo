using DemoBTL.Domain.Entity.Cerificate.Detail;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Request_Certificate
    {
        [Required(ErrorMessage ="phải nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "phải nhập mô tả")]
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
