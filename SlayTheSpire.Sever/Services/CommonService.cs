using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Services
{
    public class CommonService : ICommonService
    {
        const string SELECT_ALL_CARDTYPE = @"SELECT T.ID, T.TEXT Text FROM CARDTYPE T";
        const string SELECT_ALL_RARITY = @"SELECT T.ID, T.TEXT Text FROM RARITY T";
        const string SELECT_ALL_COLOR = @"SELECT T.ID, T.TEXT Text FROM Color T";
        private IDbContext dbContext;
        public CommonService(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public object GetAllCommon()
        {
            var types = dbContext.Query<CommonInfoModel>(SELECT_ALL_CARDTYPE);
            var rarities = dbContext.Query<CommonInfoModel>(SELECT_ALL_RARITY);
            var colors = dbContext.Query<CommonInfoModel>(SELECT_ALL_COLOR);
            return new { cardTypes = types, rarities = rarities, cardColors = colors };
        }
    }
}
