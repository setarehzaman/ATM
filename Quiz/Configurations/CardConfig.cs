
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Quiz.Entities;

namespace Quiz.Configurations
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CardNumber).HasMaxLength(16).IsRequired().IsUnicode();
            builder.Property(c => c.HolderName).IsRequired();
            builder.Property(c => c.Password).IsRequired();

            builder.HasOne(u => u.User).WithMany(c => c.UserCards).HasForeignKey(c => c.UserId);

            builder.HasData(
            new Card { Id = 1, CardNumber = "1234567812345678", HolderName = "Mellat", Balance = 500, Password = "1234", UserId = 1 },
            new Card { Id = 2, CardNumber = "8765432187654321", HolderName = "Meli", Balance = 300, Password = "5678", UserId = 2 },
            new Card { Id = 3, CardNumber = "1234567887654321", HolderName = "Saderat", Balance = 300, Password = "90-=", UserId = 3 },
            new Card { Id = 4, CardNumber = "8765432112345678", HolderName = "Sepah", Balance = 300, Password = "1234", UserId = 1 }
            );
        }
    }
}
