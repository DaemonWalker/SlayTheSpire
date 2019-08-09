using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Abstracts
{
    public interface IQueryService
    {
        public string GetName(string id);
        public string GetDescription(string id);
        public CardInfoModel GetCardInfo(string id);
        public IEnumerable<CardInfoModel> QueryCardByCondition(QueryConditionModel queryCondition);
        public IEnumerable<RelicInfoModel> QueryRelicByCondition(QueryConditionModel queryCondition);
        public RelicInfoModel GetRelicInfo(string id);
        public PotionInfoModel GetPotionInfo(string id);
        public IEnumerable<PotionInfoModel> QueryPotionByCondition(QueryConditionModel queryCondition);
    }
}
