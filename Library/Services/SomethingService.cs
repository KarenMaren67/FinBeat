using Contracts.Interfaces;
using Contracts.Dto;

namespace Library.Services;

public class SomethingService : ISomethingService
{
    private readonly ISomethingDbContext _somethingDbContext;

    public SomethingService(ISomethingDbContext somethingDbContext)
    {
        _somethingDbContext = somethingDbContext;
    }

    public async Task AddSomethingAsync(IEnumerable<SomethingAddDto> somethings, CancellationToken cancellationToken)
    {
        await _somethingDbContext.RewriteSomethingsAsync(somethings.OrderBy(x => x.Code), cancellationToken);
    }

    public async Task<SomethingPagedResult> GetSomethingsPagedAsync(SomethingPagedRequest pagedRequest,
                                                                            SomethingFilterDto? filter,
                                                                            CancellationToken cancellationToken)
    {
        var pagedResult = await _somethingDbContext.GetSomethingsAsync(pagedRequest, filter, cancellationToken);

        return pagedResult;
    }
}
