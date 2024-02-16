using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComments
{
    public class EntryCommentFavoriteEntityConfiguration : BaseEntityConfiguration<EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycommentfavorite");

            builder.HasOne(ecf => ecf.EntryComment)
                .WithMany(ec => ec.EntryCommentFavorites)
                .HasForeignKey(ecf => ecf.EntryCommentId);

            builder.HasOne(ecf => ecf.CreatedUser)
                .WithMany(u => u.EntryCommentFavorites)
                .HasForeignKey(ecf => ecf.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
