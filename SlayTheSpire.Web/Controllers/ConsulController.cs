using Microsoft.AspNetCore.Mvc;
using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Web.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class ConsulController : ControllerBase
    {
        [HttpGet]
        [Route("GetSaveService")]
        public string GetSaveSerivceUrl()
        {
            return ConsulUtils.GetSaveServiceUrl();
        }
    }
}
