using Contracts.Dto;

namespace DbContracts;

public interface ISomethingDbContext
{
    Task RewriteSomethingsAsync(IEnumerable<SomethingAddDto> somethingNew, CancellationToken cancellationToken);

    Task<SomethingPagedResult> GetSomethingsAsync(SomethingPagedRequest pagedRequest,
                                                          SomethingFilterDto? filter,
                                                          CancellationToken cancellationToken);
}
