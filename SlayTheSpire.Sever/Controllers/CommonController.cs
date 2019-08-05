using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SlayTheSpire.Sever.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Controllers
{
    [EnableCors]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        private ICommonService commonService;
        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }
        [HttpPost]
        [Route("initCommon")]
        public object InitCommon()
        {
            return commonService.GetAllCommon();
        }
    }
}
