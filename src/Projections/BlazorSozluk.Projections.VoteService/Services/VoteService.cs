﻿using BlazorSozluk.Common.Events.Entry;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.VoteService.Services
{
    public class VoteService
    {
        private readonly string connectionString;

        public VoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryVote(CreateEntryVoteEvent vote)
        {
            await DeleteEntryVote(vote.EntryId, vote.CreatedBy);

            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO entryvote (\"Id\", \"CreateDate\", \"EntryId\", \"VoteType\", \"CreatedById\") VALUES (@Id, NOW(), @EntryId, @VoteType, @CreatedById)",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = vote.EntryId,
                    VoteType = (int)vote.VoteType,
                    CreatedById = vote.CreatedBy
                });
        }

        public async Task DeleteEntryVote(Guid entryId, Guid userId)
        {
            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM entryvote WHERE \"EntryId\" = @EntryId AND \"CreatedById\" = @UserId",
                new
                {
                    EntryId = entryId,
                    UserId = userId
                });
        }
    }
}
