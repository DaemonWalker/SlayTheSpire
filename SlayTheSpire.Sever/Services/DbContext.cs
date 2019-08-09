using Dapper;
using Microsoft.Extensions.Configuration;
using SlayTheSpire.Sever.Abstracts;
using SlayTheSpire.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            //Dapper.SqlMapper.SetTypeMap(typeof(CommonInfoModel),
            //    new CustomPropertyTypeMap(typeof(CommonInfoModel),
            //    (type, columnName) =>
            //    {
            //        var p = type.GetProperties().FirstOrDefault(prop => prop.Name.ToLower().Replace("_", "") == columnName.ToLower().Replace("_", ""));
            //        Console.WriteLine($"\t\t\t{p}");
            //        return p;
            //    }));
            this.config = config;
            dbConnection = new SQLiteConnection(this.config.GetConnectionString("SQLite"));
            dbConnection.Open();
        }
        public IEnumerable<T> Query<T>(string sql, IEnumerable<DbParameter> parms)
        {
            if (parms != null)
            {
                var args = this.ConvertParm(parms);
                return dbConnection.Query<T>(sql, args);
            }
            else
            {
                return dbConnection.Query<T>(sql);
            }
        }
        public IEnumerable<T> Query<T>(string sql, object parm)
        {
            return dbConnection.Query<T>(sql, param: parm);
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
        public T ExcuteScalar<T>(string sql, object parm)
        {
            return dbConnection.Query<T>(sql, parm).SingleOrDefault();
        }
        public DbParameter CreateParameter(string name, object value, DbType dbType)
        {
            if (dbType == DbType.Int32)
            {
                if (string.IsNullOrWhiteSpace(Convert.ToString(value)))
                {
                    value = 0;
                }
                else
                {
                    if (int.TryParse(Convert.ToString(value), out var tempInt))
                    {
                        value = tempInt;
                    }
                    else
                    {
                        value = 0;
                    }
                }
            }
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
