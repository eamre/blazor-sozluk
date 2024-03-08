using BlazorSozluk.Common.Events.User;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Projections.UserService.Services
{
    public class UserService
    {
        private readonly string connectionString;

        public UserService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("BlazorSozlukDbConnectionString");
        }

        public async Task<Guid> CreateEmailConfirmation(UserEmailChangedEvent @event)
        {
            var guid = Guid.NewGuid();

            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync("INSERT INTO EMAILCONFIRMATION (\"Id\", \"CreateDate\", \"OldEmailAddress\", \"NewEmailAddress\") VALUES (@Id, NOW(), @OldEmailAddress, @NewEmailAddress)",
                new
                {
                    Id = guid,
                    OldEmailAddress = @event.OldEmailAddress,
                    NewEmailAddress = @event.NewEmailAddress
                });

            return guid;

        }
    }
}
