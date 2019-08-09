using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Abstracts
{
    public interface IDbContext
    {
        public IEnumerable<T> Query<T>(string sql, IEnumerable<DbParameter> parms);
        public IEnumerable<T> Query<T>(string sql, object parm);
        public T ExcuteScalar<T>(string sql, IEnumerable<DbParameter> parms);
        public T ExcuteScalar<T>(string sql, object parm);
        public string ExcuteScalar(string sql, IEnumerable<DbParameter> parms);
        public DbParameter CreateParameter(string name, object value, DbType dbType);
        public IEnumerable<T> Query<T>(string sql)
        {
            return this.Query<T>(sql, null);
        }
        public List<DbParameter> CreateParamList()
        {
            return new List<DbParameter>();
        }
    }
}
