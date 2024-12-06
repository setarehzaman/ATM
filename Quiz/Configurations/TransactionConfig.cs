using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Quiz.Entities;

namespace Quiz.Configurations
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionId);

            builder.Property(t => t.SourceCardNumber).HasMaxLength(16).IsRequired();
            builder.Property(t => t.DestinationCardNumber).HasMaxLength(16).IsRequired();

            builder.HasOne(t => t.SourceCard).WithMany(c => c.SourceCardTransactions)
                .HasForeignKey(t => t.SourceCardId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.DestinationCard).WithMany(c => c.DestinationCardTransactions)
                .HasForeignKey(t => t.DestinationCardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
