using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.Entries
{
    public class EntryFavoriteEntityConfiguration : BaseEntityConfiguration<EntryFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entryfavorite", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(ef => ef.Entry)
                .WithMany(e => e.EntryFavorites)
                .HasForeignKey(ef => ef.EntryId);

            builder.HasOne(ef => ef.CreatedUser)
                .WithMany(e => e.EntryFavorites)
                .HasForeignKey(ef => ef.CreatedById);

        }
    }
}
