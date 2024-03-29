﻿namespace ThorAdmin.Services;

public interface IMySqlService
{
    Task<TData> ExecuteSaclar<TData>(string sql, string server, string userName, string password, string database = "mysql");

    Task<int> ExecuteNonQuery(string sql, string server, string userName, string password, string database = "mysql");

    Task<bool> DatabaseExists(string databaseName, string server, string userName, string password);

    Task<bool> TableExists(string tableName, string databaseName, string server, string userName, string password);

    Task<bool> CreateDatabase(string databaseName, string server, string userName, string password);

    Task<bool> DeleteDatabase(string databaseName, string server, string userName, string password);
}
