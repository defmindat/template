using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EatWise.Common.Infrastructure.Inbox;

public sealed class InboxMessageConfiguration: IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable("inbox_messages");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Content).HasMaxLength(2000).HasColumnType("jsonb");
    }
}
