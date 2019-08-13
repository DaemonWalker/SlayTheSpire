using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Web.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController:ControllerBase
    {
        /// <summary>
        /// Consul健康检查接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ok()
        {
            return Ok("ok");
        }
    }
}
