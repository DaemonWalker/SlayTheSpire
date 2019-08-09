using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Shared;

namespace SlayTheSpire.Sever.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CheaterController : ControllerBase
    {
        private readonly ISaveCheater saveCheater;
        public CheaterController(ISaveCheater saveCheater)
        {
            this.saveCheater = saveCheater;
        }

        [HttpPost]
        [Route("upload")]
        public ServerSaveModel UploadSave([FromBody] string save)
        {
            return saveCheater.GetSaveModel(save);
        }

        [HttpPost]
        [Route("generate")]
        public string GenerateSave([FromBody] SaveModel save)
        {
            return saveCheater.GenerateSave(save);
        }

        [HttpPost]
        [Route("export")]
        [Produces("application/x-zip-compressed")]
        public IActionResult Export([FromBody]ExportModel model)
        {
            var bytes = saveCheater.GenerateZip(model);
            //Response.Headers.Add("content-disposition", "attachment; filename=save.zip; filename*=UTF-8''save.zip");
            return File(bytes, "application/x-zip-compressed", "save.zip");
        }
    }
}
