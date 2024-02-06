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
    public class EntryCommentEntityConfiguration : BaseEntityConfiguration<EntryComment>
    {
        public override void Configure(EntityTypeBuilder<EntryComment> builder)
        {
            base.Configure(builder);

            builder.ToTable("entrycomment", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(ec => ec.CreatedBy)
                .WithMany(u => u.EntryComments)
                .HasForeignKey(ec => ec.CreatedById);

            builder.HasOne(ec => ec.Entry)
                .WithMany(e => e.EntryComments)
                .HasForeignKey(ec => ec.EntryId);

        }
    }
}
