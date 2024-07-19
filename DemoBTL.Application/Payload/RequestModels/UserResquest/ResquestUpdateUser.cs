using DemoBTL.Domain.Entity.Enumerates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class ResquestUpdateUser
    {
        public IFormFile Avatar { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }

    }
}
