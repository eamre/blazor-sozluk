using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.FavoriteService.Services
{
    public class FavoriteService
    {
        private readonly string connectionString;

        public FavoriteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryFav(CreateEntryFavEvent @event)
        {
            using var connection = new NpgsqlConnection(connectionString);

            await connection
                .ExecuteAsync("INSERT INTO entryfavorite (\"Id\", \"EntryId\", \"CreatedById\", \"CreateDate\") VALUES(@Id, @EntryId, @CreatedById, NOW())",
            new
            {
                Id = Guid.NewGuid(),
                EntryId = @event.EntryId,
                CreatedById = @event.CreatedBy
            });

        }
        public async Task CreateEntryCommentFav(CreateEntryCommentFavEvent @event)
        {
            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO entrycommentfavorite (\"Id\", \"EntryCommentId\", \"CreatedById\", \"CreateDate\") VALUES(@Id, @EntryCommentId, @CreatedById, NOW())",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryCommentId = @event.EntryCommentId,
                    CreatedById = @event.CreatedBy
                });
        }
        public async Task DeleteEntryFav(DeleteEntryFavEvent @event)
        {
            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryFavorite WHERE \"EntryId\" = @EntryId AND \"CreatedById\" = @CreatedById",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = @event.EntryId,
                    CreatedById = @event.CreatedBy
                });
        }

        public async Task DeleteEntryCommentFav(DeleteEntryCommentFavEvent @event)
        {
            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM EntryCommentFavorite WHERE \"EntryCommentId\" = @EntryCommentId AND \"CreatedById\" = @CreatedById",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryCommentId = @event.EntryCommentId,
                    CreatedById = @event.CreatedBy
                });
        }
    }
}
