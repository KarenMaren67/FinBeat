using Contracts.Dto;

namespace Contracts.Interfaces;

public interface ISomethingService
{
    Task AddSomethingAsync(IEnumerable<SomethingAddDto> something, CancellationToken cancellationToken);
    Task<SomethingPagedResult> GetSomethingsPagedAsync(SomethingPagedRequest pagedRequest,
                                                               SomethingFilterDto? filter,
                                                               CancellationToken cancellationToken);
}
