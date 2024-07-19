using DemoBTL.Application.Payload.RequestModels.UserResquest;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Application.InterfaceService
{
    public interface ICouseService
    {
        Task<string> AddCouse(int UserId, Request_Couse request_Couse);
    }
}
