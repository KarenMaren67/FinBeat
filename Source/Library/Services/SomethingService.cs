using Contracts.Dto;
using DB.DbContexts.Linq2DbContexts;

namespace Library.Services;

public class SomethingService : ISomethingService
{
    private readonly SomethingDataContext _somethingDаtaContext;

    public SomethingService(SomethingDataContext somethingDataContext)
    {
        _somethingDаtaContext = somethingDataContext;
    }

    public async Task AddSomethingAsync(IEnumerable<SomethingAddDto> somethings, CancellationToken cancellationToken)
    {
        await _somethingDаtaContext.RewriteSomethingsAsync(somethings.OrderBy(x => x.Code), cancellationToken);
    }

    public async Task<SomethingPagedResult> GetSomethingsPagedAsync(SomethingPagedRequest pagedRequest,
                                                                            SomethingFilterDto? filter,
                                                                            CancellationToken cancellationToken)
    {
        var pagedResult = await _somethingDаtaContext.GetSomethingsAsync(pagedRequest, filter, cancellationToken);

        return pagedResult;
    }
}
