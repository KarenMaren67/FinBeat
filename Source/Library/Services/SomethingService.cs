using Contracts.Dto;
using Contracts.Interfaces;
using DB.DbContexts.Linq2DbContexts;
using System.Text.Json;

namespace Library.Services;

public class SomethingService : ISomethingService
{
    private readonly SomethingDataContext _somethingDаtaContext;
    private readonly IKafkaProducerService _kafkaProducerService;

    public SomethingService(SomethingDataContext somethingDataContext, IKafkaProducerService kafkaProducerService)
    {
        _somethingDаtaContext = somethingDataContext;
        _kafkaProducerService = kafkaProducerService;
    }

    public async Task AddSomethingAsync(IEnumerable<SomethingAddDto> somethings, CancellationToken cancellationToken)
    {
        await _somethingDаtaContext.RewriteSomethingsAsync(somethings.OrderBy(x => x.Code), cancellationToken);
        await _kafkaProducerService.ProduceMessageAsync(JsonSerializer.Serialize(somethings.ToArray()));
    }

    public async Task<SomethingPagedResult> GetSomethingsPagedAsync(SomethingPagedRequest pagedRequest,
                                                                    SomethingFilterDto? filter,
                                                                    CancellationToken cancellationToken)
        => await _somethingDаtaContext.GetSomethingsAsync(pagedRequest, filter, cancellationToken);
}
