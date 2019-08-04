using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Abstracts
{
    public interface ICardService
    {
        public string GetName(string id);
        public string GetDescription(string id);
        public CardInfoModel GetCardInfo(string id);
    }
}
