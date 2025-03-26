using Dapper;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SQLDataLibrary
{
	public class SQLDataAccess
	{
		public List<T> LoadData<T, U>(string sql, U parameters, string connectionString)
		{
			using (System.Data.IDbConnection connection = new SqlConnection(connectionString))
			{
				var rows = connection.Query<T>(sql, parameters).ToList();
				return rows;
			}
		}

		public void SaveData<T>(string sql, T parameters, string connectionString)
		{
			using (System.Data.IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Execute(sql, parameters);
			}
		}
	}
}
