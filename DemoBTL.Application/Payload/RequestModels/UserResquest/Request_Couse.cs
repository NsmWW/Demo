using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Application.Payload.RequestModels.UserResquest
{
    public class Request_Couse
    {
        [Required(ErrorMessage ="Tên khoá học")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Giới thiệu khoá học")]
        public string Introduce { get; set; }
        [Required(ErrorMessage = "Ảnh")]
        public string? ImageCouse { get; set; }
        [Required(ErrorMessage = "không hiểu")]
        public string? Code { get; set; }
        [Required(ErrorMessage = "Giá khoá học")]
        public decimal? Price { get; set; }
        public int? TotalCourseDuration { get; set; }

    }
}
