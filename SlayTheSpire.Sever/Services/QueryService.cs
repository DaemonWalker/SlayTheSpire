using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Services
{
    public class QueryService : IQueryService
    {
        const string DESCRIPTION_SQL = @"select description from card t where t.id=@id";
        const string NAME_SQL = @"select name from card t where t.id=@id";
        const string INFO_SQL = @"select Name,Description from card t where t.id=@id";
        const string QUERY_CARD = @"
SELECT
	t.ID,
	t.Name,
	t.Description,
	t1.Text CardType,
	t2.Text Color,
	t3.Text Rarity 
FROM
	card t
	INNER JOIN cardtype t1 ON t.Type = t1.ID
	INNER JOIN color t2 ON t.Color = t2.ID
	INNER JOIN rarity t3 ON t.Rarity = t3.ID 
WHERE
	( t.Color =@Color OR @Color = 0 ) 
	AND ( @Rarity = 0 OR t.Rarity =@Rarity ) 
	AND ( @CardType = 0 OR t.Type =@CardType ) 
	AND ( t.Name LIKE '%' ||@Name || '%' ) 
	AND ( t.Description LIKE '%' ||@Description || '%' )";
        const string QUERY_RELIC = @"
SELECT
	t.ID,
	t.Name,
	t.Description,
	t.Flavor,
	t3.Text Rarity 
FROM
	Relic t
	INNER JOIN rarity t3 ON t.Rarity = t3.ID 
WHERE
	( @Rarity = 0 OR t.Rarity =@Rarity ) 
	AND ( t.Name LIKE '%' || @Name || '%' ) 
	AND ( t.Description LIKE '%' || @Description || '%' ) 
	AND ( t.Flavor LIKE '%' || @Flavor || '%')";
        const string QUERY_RELIC_BY_ID = @"select t.ID,t.Description,t.Name from Relic t where t.ID=@id";
        const string QUERY_POTION_BY_ID = @"SELECT t.Name,t.ID,t.Description FROM Potions T WHERE T.ID=@id";
        const string QUERY_POTION = @"
SELECT
	t.Name,
	t.ID,
	t.Description 
FROM
	Potions T 
WHERE
	t.Name LIKE '%' || @Name || '%' 
	AND t.Description LIKE '%' ||@Description || '%'";
        private IDbContext dbContext;
        public QueryService(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string GetDescription(string id)
        {
            return dbContext.ExcuteScalar<string>(DESCRIPTION_SQL, new { id });
        }

        public string GetName(string id)
        {
            return dbContext.ExcuteScalar<string>(DESCRIPTION_SQL, new { id });
        }

        public CardInfoModel GetCardInfo(string id)
        {
            return dbContext.ExcuteScalar<CardInfoModel>(INFO_SQL, new { id });
        }

        public IEnumerable<CardInfoModel> QueryCardByCondition(QueryConditionModel queryCondition)
        {
            //var list = dbContext.CreateParamList();
            //list.Add(dbContext.CreateParameter("Color", queryCondition.Color, DbType.Int32));
            //list.Add(dbContext.CreateParameter("Rarity", queryCondition.Rarity, DbType.Int32));
            //list.Add(dbContext.CreateParameter("CardType", queryCondition.CardType, DbType.Int32));
            //list.Add(dbContext.CreateParameter("Name", queryCondition.Name, DbType.String));
            //list.Add(dbContext.CreateParameter("Description", queryCondition.Description, DbType.String));
            //return dbContext.Query<CardInfoModel>(QUERY_CARD, list);
            return dbContext.Query<CardInfoModel>(QUERY_CARD, queryCondition);
        }
        public IEnumerable<RelicInfoModel> QueryRelicByCondition(QueryConditionModel queryCondition)
        {
            return dbContext.Query<RelicInfoModel>(QUERY_RELIC, queryCondition);
        }
        public RelicInfoModel GetRelicInfo(string id)
        {
            return dbContext.ExcuteScalar<RelicInfoModel>(QUERY_RELIC_BY_ID, new { id });
        }
        public PotionInfoModel GetPotionInfo(string id)
        {
            return dbContext.ExcuteScalar<PotionInfoModel>(QUERY_POTION_BY_ID, new { id });
        }
        public IEnumerable<PotionInfoModel> QueryPotionByCondition(QueryConditionModel queryCondition)
        {
            return dbContext.Query<PotionInfoModel>(QUERY_POTION, queryCondition);
        }
    }
}
