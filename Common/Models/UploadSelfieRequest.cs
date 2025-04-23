using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Models
{
    public class UploadSelfieRequest
    {
        [FromForm(Name = "selfie")]
        public IFormFile Selfie { get; set; }

        [FromForm(Name = "userId")]
        public string UserId { get; set; }
    }

}
