using Contracts.Dto;
using Contracts.Interfaces;
using Dapper;
using DB.Configuration;
using DB.Entities;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Text;

namespace DB.DbContexts;

public class SomethingDbContext : ISomethingDbContext
{
    private const string SOMETHING_TABLE_NAME = "somethings";
    private readonly string _connectionString;

    public SomethingDbContext(IOptions<PgSqlDbConfiguration> databaseConfigurationOption)
    {
        _connectionString = databaseConfigurationOption.Value.ConnectionString;
    }

    public async Task RewriteSomethingsAsync(IEnumerable<SomethingAddDto> somethingNew, CancellationToken cancellationToken = default)
    {
        var deleteSql = $"TRUNCATE TABLE {SOMETHING_TABLE_NAME} RESTART IDENTITY";
        string insertSql = $"INSERT INTO {SOMETHING_TABLE_NAME} (code, value) VALUES (@Code, @Value)";

        using (IDbConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                
                var deleteCommand = new CommandDefinition(deleteSql, transaction: transaction, cancellationToken: cancellationToken);
                var insertCommand = new CommandDefinition(insertSql, somethingNew, transaction: transaction, cancellationToken: cancellationToken);

                try
                {
                    await connection.ExecuteAsync(deleteCommand);
                    await connection.ExecuteAsync(insertCommand);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public async Task<SomethingPagedResult> GetSomethingsAsync(SomethingPagedRequest pagedRequest,
                                                                       SomethingFilterDto? filter,
                                                                       CancellationToken cancellationToken = default)
    {
        var filterExpression = GetFilterExpression(filter);

        var filteredSql = $"""
                              SELECT *
                              FROM {SOMETHING_TABLE_NAME}
                              WHERE {filterExpression}
                              ORDER BY id
                          """;

        var pagedSql = $"""
                            {filteredSql}
                            LIMIT @Limit
                            OFFSET @Offset
                        """;

        var totalCountSql = $"""
                                SELECT count(*) AS TotalCount
                                FROM ({filteredSql}) tmp
                            """;

        var parameters = new
        {
            Limit = pagedRequest.PageItemsCount,
            Offset = (pagedRequest.PageNumber - 1) * pagedRequest.PageItemsCount
        };

        using (IDbConnection connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                var getPagedCommand = new CommandDefinition(pagedSql, parameters, transaction: transaction, cancellationToken: cancellationToken);
                var getTotalCountCommand = new CommandDefinition(totalCountSql, transaction: transaction, cancellationToken: cancellationToken);

                var entities = await connection.QueryAsync<SomethingEntity>(getPagedCommand);
                var totalCount = await connection.QuerySingleOrDefaultAsync<int>(getTotalCountCommand);

                transaction.Commit();
                return AsPagedResult(entities, totalCount);
            }
        }
    }

    private string GetFilterExpression(SomethingFilterDto? filter)
    {
        var filterBuilder = new StringBuilder("1 = 1");

        if (filter == null)
            return filterBuilder.ToString();

        if (filter.MinimalCode.HasValue)
            filterBuilder.Append($" AND code >= {filter.MinimalCode.Value}");

        if (filter.MaximalCode.HasValue)
            filterBuilder.Append($" AND code <= {filter.MaximalCode.Value}");

        if (!string.IsNullOrEmpty(filter.ValueContains))
            filterBuilder.Append($" AND UPPER(value) LIKE UPPER('%{filter.ValueContains}%')");

        return filterBuilder.ToString();
    }

    private static SomethingPagedResult AsPagedResult(IEnumerable<SomethingEntity> somethingEntities, int totalCount) =>
        new SomethingPagedResult(
            somethingEntities
                .Select(x => new SomethingGetDto(x.Id, x.Code, x.Value ?? string.Empty))
                .ToArray(),
            totalCount);
}
