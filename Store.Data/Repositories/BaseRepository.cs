using Microsoft.Data.SqlClient;
using System.Data;

namespace Store.Data.Repositories
{
    public abstract class BaseRepository
    {
        private readonly IDbConnection _dbConnection;

        protected BaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        protected async Task OpenConnectionAsync()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                await ((SqlConnection)_dbConnection).OpenAsync();
            }
        }

        protected async Task CloseConnectionAsync()
        {
            if (_dbConnection.State != ConnectionState.Closed)
            {
                await ((SqlConnection)_dbConnection).CloseAsync();
            }
        }

        protected void AddParameter(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = name;
            parameter.Value = value;

            command.Parameters.Add(parameter);
        }

        protected IDbCommand CreateCommand()
        {
            return _dbConnection.CreateCommand();
        }

        protected async Task<int> ExecuteNonQueryAsync(IDbCommand command)
        {
            return await ((SqlCommand)command).ExecuteNonQueryAsync();
        }

        protected async Task<object?> ExecuteScalarAsync(IDbCommand command)
        {
            return await ((SqlCommand)command).ExecuteScalarAsync();
        }

        protected async Task<IDataReader> ExecuteReaderAsync(IDbCommand command)
        {
            return await ((SqlCommand)command).ExecuteReaderAsync();
        }
    }
}
