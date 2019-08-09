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
    [Route("api")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class QueryController : ControllerBase
    {
        private IQueryService queryService;
        public QueryController(IQueryService cardService)
        {
            this.queryService = cardService;
        }

        [HttpPost]
        [Route("card/Query")]
        public IEnumerable<CardInfoModel> CardInfo([FromBody] QueryConditionModel queryCondition)
        {
            return queryService.QueryCardByCondition(queryCondition);
        }

        [HttpPost]
        [Route("relic/query")]
        public IEnumerable<RelicInfoModel> QueryRelicByCondition([FromBody] QueryConditionModel queryCondition)
        {
            return queryService.QueryRelicByCondition(queryCondition);
        }

        [HttpPost]
        [Route("potion/query")]
        public IEnumerable<PotionInfoModel> QueryPotionByCondition([FromBody] QueryConditionModel queryCondition)
        {
            return queryService.QueryPotionByCondition(queryCondition);
        }
    }
}
