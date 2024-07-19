using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DemoBTL.Application.Handle.HandleFile
{
    public class HandleeUploadFile
    {
        public static async Task<string> WriteFile(IFormFile file)
        {
            string fileName = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = "Mybugs_" + DateTime.Now.Ticks + extension;
                var filePart = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "files");
                if (!Directory.Exists(filePart))
                {
                    Directory.CreateDirectory(filePart);
                }
                var exacPath = Path.Combine(filePart,fileName);
                using(var steam = new FileStream(exacPath, FileMode.Create))
                {
                    await file.CopyToAsync(steam);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return fileName;
        }
    }
}
