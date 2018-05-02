using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheMatrix.Controllers
{
    [Produces("application/json")]
    [Route("api/Fallback")]
    public class FallbackController : Controller
    {
        public PhysicalFileResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html"), "text/html");
        }
    }
}