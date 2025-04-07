using Contracts.Dto;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace VoskanyanFinBeatApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SomethingController : ControllerBase
{
    private readonly ISomethingService _somethingService;

    public SomethingController(ISomethingService somethingService)
    {
        _somethingService = somethingService;
    }

    [HttpPost("Rewrite")]
    public async Task AddSomethingAsync([FromBody] IEnumerable<SomethingAddDto> input) =>
        await _somethingService.AddSomethingAsync(input, HttpContext.RequestAborted);

    [HttpGet("GetPaged")]
    public async Task<SomethingPagedResult> GetSomethingPagedAsync([FromQuery] int pageNumber,
                                                                           [FromQuery] int pageItemsCount,
                                                                           [FromQuery] int? minimalCode,
                                                                           [FromQuery] int? maximalCode,
                                                                           [FromQuery] string? valueContains) =>
        await _somethingService.GetSomethingsPagedAsync(new SomethingPagedRequest(pageNumber, pageItemsCount),
                                                        new SomethingFilterDto(minimalCode, maximalCode, valueContains),
                                                        HttpContext.RequestAborted);
}
