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
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CardController : ControllerBase
    {
        private ICardService cardService;
        public CardController(ICardService cardService)
        {
            this.cardService = cardService;
        }

        [HttpPost]
        [Route("GetName")]
        public string GetName([FromBody] string id)
        {
            return cardService.GetName(id);
        }

        [HttpPost]
        [Route("GetDescription")]
        public string GetDescription([FromBody] string id)
        {
            return cardService.GetDescription(id);
        }
    }
}
