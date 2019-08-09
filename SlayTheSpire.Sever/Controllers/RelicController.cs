using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RelicController : ControllerBase
    {
        private IQueryService queryService;
        public RelicController(IQueryService queryService)
        {
            this.queryService = queryService;
        }
        [HttpPost]
        [Route("query")]
        public IEnumerable<RelicInfoModel> QueryRelicByCondition([FromBody] QueryConditionModel queryCondition)
        {
            return queryService.QueryRelicByCondition(queryCondition);
        }
    }
}
