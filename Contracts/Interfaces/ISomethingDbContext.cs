using Contracts.Dto;

namespace Contracts.Interfaces;

public interface ISomethingDbContext
{
    Task RewriteSomethingsAsync(IEnumerable<SomethingAddDto> somethingNew, CancellationToken cancellationToken);

    Task<SomethingPagedResult> GetSomethingsAsync(SomethingPagedRequest pagedRequest,
                                                          SomethingFilterDto? filter,
                                                          CancellationToken cancellationToken);
}
