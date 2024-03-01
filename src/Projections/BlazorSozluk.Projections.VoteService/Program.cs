using BlazorSozluk.Projections.VoteService;
using Dapper;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

DefaultTypeMap.MatchNamesWithUnderscores = true;

await host.RunAsync();
