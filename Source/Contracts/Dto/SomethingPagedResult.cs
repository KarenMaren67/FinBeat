namespace Contracts.Dto;

public record SomethingPagedResult(IReadOnlyList<SomethingGetDto> Somethings, int TotalCount);
