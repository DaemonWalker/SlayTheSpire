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
    public class CardService : ICardService
    {
        const string DESCRIPTION_SQL = @"select description from card t where t.id=@id";
        const string NAME_SQL = @"select name from card t where t.id=@id";
        const string INFO_SQL = @"select Name,Description from card t where t.id=@id";
        private IDbContext dbContext;
        public CardService(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string GetDescription(string id)
        {
            var list = dbContext.CreateParamList();
            list.Add(dbContext.CreateParameter("id", id, DbType.String));
            return dbContext.ExcuteScalar(DESCRIPTION_SQL, list);
        }

        public string GetName(string id)
        {
            var list = dbContext.CreateParamList();
            list.Add(dbContext.CreateParameter("id", id, DbType.String));
            return dbContext.ExcuteScalar(NAME_SQL, list);
        }

        public CardInfoModel GetCardInfo(string id)
        {
            var list = dbContext.CreateParamList();
            list.Add(dbContext.CreateParameter("id", id, DbType.String));
            return dbContext.ExcuteScalar<CardInfoModel>(INFO_SQL, list);
        }
    }
}
