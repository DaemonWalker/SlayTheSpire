using Dapper;
using Microsoft.Extensions.Configuration;
using SlayTheSpire.Sever.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Services
{
    public class DbContext : IDbContext
    {
        private IConfiguration config;
        private IDbConnection dbConnection;
        public DbContext(IConfiguration config)
        {
            this.config = config;
            dbConnection = new SQLiteConnection(this.config.GetConnectionString("SQLite"));
            dbConnection.Open();
        }
        public IEnumerable<T> Query<T>(string sql, IEnumerable<DbParameter> parms)
        {
            var args = this.ConvertParm(parms);
            return dbConnection.Query<T>(sql, args);
        }
        public T ExcuteScalar<T>(string sql, IEnumerable<DbParameter> parms)
        {
            var args = this.ConvertParm(parms);
            return dbConnection.Query<T>(sql, args).SingleOrDefault();
        }
        public string ExcuteScalar(string sql, IEnumerable<DbParameter> parms)
        {
            var comm = this.dbConnection.CreateCommand();
            comm.CommandText = sql;
            parms.ToList().ForEach(p => comm.Parameters.Add(p));
            return Convert.ToString(comm.ExecuteScalar());
        }
        public DbParameter CreateParameter(string name, object value, DbType dbType)
        {
            return new SQLiteParameter()
            {
                DbType = dbType,
                Value = value,
                ParameterName = name
            };
        }

        private DynamicParameters ConvertParm(IEnumerable<DbParameter> parms)
        {
            var args = new DynamicParameters();
            foreach (var item in parms)
            {
                args.Add(name: item.ParameterName, value: item.Value, dbType: item.DbType);
            }
            return args;
        }
    }
}
