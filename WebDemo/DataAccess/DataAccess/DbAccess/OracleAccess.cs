using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;


namespace DataAccess.DbAccess;

internal class OracleAccess : IOracleAccess
{
	private readonly IConfiguration _config;
	public OracleAccess(IConfiguration config)
	{
		_config = config;
	}
	public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure,
												 U parameters,
												 string connectionId = "Default")
	{
		using IDbConnection connection = new OracleConnection(_config.GetConnectionString(connectionId));
		return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}

	public async Task SaveData<T>(string storedProcedure,
												 T parameters,
												 string connectionId = "Default")
	{
		using IDbConnection connection = new OracleConnection(_config.GetConnectionString(connectionId));
		await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}
}
