using Microsoft.Extensions.DependencyInjection;
using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Sever.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Extenssions
{
    public static class ExtenssionMethods
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ISaveCheater, SaveCheater>();
            services.AddSingleton<IDbContext, DbContext>();
            services.AddSingleton<ICardService, CardService>();
            services.AddSingleton<ICommonService, CommonService>();
        }
    }
}
