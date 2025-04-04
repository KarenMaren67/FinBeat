namespace Contracts.Dto;

public record SomethingPagedResult(IReadOnlyList<SomethingGetDto> somethings, int TotalCount);
