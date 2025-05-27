using Contracts.Dto;
using DbContracts;
using DbContracts.Entities;
using LinqToDB;
using LinqToDB.Data;
using System.Data;
using System.Linq.Expressions;

namespace DB.DbContexts.Linq2DbContexts;

public class SomethingDataContext : DataConnection, ISomethingDbContext
{
    public SomethingDataContext(DataOptions<SomethingDataContext> options)
        : base(options.Options)
    {
        this.CreateTable<Something>(tableOptions: TableOptions.CreateIfNotExists);
    }

    public async Task RewriteSomethingsAsync(IEnumerable<SomethingAddDto> somethingNew, CancellationToken cancellationToken = default)
    {
        var entities = somethingNew.Select(x => new Something { Code = x.Code, Value = x.Value }).ToArray();

        using (var transaction = Connection.BeginTransaction(IsolationLevel.Serializable))
        {
            try
            {
                var somethings = this.GetTable<Something>();

                await somethings.TruncateAsync<Something>(token: cancellationToken);
                await somethings.BulkCopyAsync(new BulkCopyOptions{ BulkCopyType = BulkCopyType.MultipleRows}, entities, cancellationToken);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    public async Task<SomethingPagedResult> GetSomethingsAsync(SomethingPagedRequest pagedRequest,
                                                               SomethingFilterDto? filter,
                                                               CancellationToken cancellationToken = default)
    {
        var limit = pagedRequest.PageItemsCount;
        var offset = (pagedRequest.PageNumber - 1) * pagedRequest.PageItemsCount;

        using (var transaction = Connection.BeginTransaction(IsolationLevel.Serializable))
        {
            var somethings = this.GetTable<Something>();
            var filteredEntities = filter != null
                ? GetFilteredEntities(somethings, filter)
                : somethings;

            var totalCount = filteredEntities.Count();
            var pagedResult = filteredEntities.Skip(offset < 0 ? 0 : offset);
            
            if (limit > 1)
            {
                pagedResult.Take(limit);
            }

            var result = pagedResult.ToArray();

            transaction.Commit();
            return AsPagedResult(pagedResult, totalCount);
        }
    }

    private IQueryable<Something> GetFilteredEntities(ITable<Something> somethings, SomethingFilterDto filter)
    {
        var filtered = somethings as IQueryable<Something>;

        if (filter.MinimalCode.HasValue)
            filtered = filtered.Where(x => x.Code >= filter.MinimalCode.Value);

        if (filter.MaximalCode.HasValue)
            filtered = filtered.Where(x => x.Code <= filter.MaximalCode.Value);

        if (!string.IsNullOrEmpty(filter.ValueContains))
            filtered = filtered.Where(x => x.Value.ToUpper().Contains(filter.ValueContains.ToUpper()));

        return filtered;
    }

    private Expression<Func<Something, bool>> NewMethod(SomethingFilterDto filter, Func<Something, bool> filterFunc)
    {
        return (x) => filterFunc(x) && x.Code >= filter.MinimalCode.Value;
    }

    private static SomethingPagedResult AsPagedResult(IEnumerable<Something> somethingEntities, int totalCount) =>
        new SomethingPagedResult(
            somethingEntities
                .Select(x => new SomethingGetDto(x.Id, x.Code, x.Value ?? string.Empty))
                .ToArray(),
            totalCount);
}
