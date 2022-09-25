using MySql.Data.MySqlClient;
using System.Data;

namespace ThorAdmin.Services;

public class MySqlService: IMySqlService
{
    async Task<MySqlConnection> GetConnection(string server, string userName, string password, string database = "mysql")
    {
        var connection = new MySqlConnection($"server={server}; uid={userName}; pwd={password}; database={database}");
        await connection.OpenAsync();
        return connection;
    }

    public async Task<TData> ExecuteSaclar<TData>(string sql, string server, string userName, string password, string database = "mysql")
    {
        using var connection = await GetConnection(server, userName, password, database);

        var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = sql;
        var value = await command.ExecuteScalarAsync();
        if (value == null)
            return default;

        return (TData)value;
    }

    public async Task<bool> DatabaseExists(string databaseName, string server, string userName, string password)
    {
        var sql = $"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{databaseName}'";

        var dbName = await ExecuteSaclar<string>(sql, server, userName, password);
        if (dbName == null)
            return false;

        return dbName.Equals(databaseName);
    }

}
