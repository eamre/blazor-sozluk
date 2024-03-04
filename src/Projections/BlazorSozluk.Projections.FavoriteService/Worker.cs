using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;

namespace BlazorSozluk.Projections.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration configuration;
        public Worker(ILogger<Worker> logger, IConfiguration configuration = null)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = configuration.GetConnectionString("BlazorSozlukDbConnectionString");

            var favService = new Services.FavoriteService(connStr);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.FavExchangeName)
                .EnsureQueue(SozlukConstants.CreateEntryFavQueueName, SozlukConstants.FavExchangeName)
                .Receive<CreateEntryFavEvent>(async fav =>
                {
                    //db insert
                    favService.CreateEntryFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Received EntryId {fav.EntryId}");

                }).StartConsuming(SozlukConstants.CreateEntryFavQueueName);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.FavExchangeName)
                .EnsureQueue(SozlukConstants.DeleteEntryFavQueueName, SozlukConstants.FavExchangeName)
                .Receive<DeleteEntryFavEvent>(async fav =>
                {
                    //db insert
                    favService.DeleteEntryFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Delete Received EntryId {fav.EntryId}");

                }).StartConsuming(SozlukConstants.DeleteEntryFavQueueName);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.FavExchangeName)
                .EnsureQueue(SozlukConstants.CreateEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
                .Receive<CreateEntryCommentFavEvent>(async fav =>
                {
                    //db insert
                    favService.CreateEntryCommentFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Create EntryComment Received EntryCommentId {fav.EntryCommentId}");

                }).StartConsuming(SozlukConstants.CreateEntryCommentFavQueueName);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(SozlukConstants.FavExchangeName)
                .EnsureQueue(SozlukConstants.DeleteEntryCommentFavQueueName, SozlukConstants.FavExchangeName)
                .Receive<DeleteEntryCommentFavEvent>(async fav =>
                {
                    //db insert
                    favService.DeleteEntryCommentFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Delete Received EntryCommentId {fav.EntryCommentId}");

                }).StartConsuming(SozlukConstants.DeleteEntryCommentFavQueueName);
        }
    }
}