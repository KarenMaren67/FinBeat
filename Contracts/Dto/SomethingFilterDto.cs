namespace Contracts.Dto;

public record SomethingFilterDto(int? MinimalCode,
                                 int? MaximalCode,
                                 string? ValueContains);
