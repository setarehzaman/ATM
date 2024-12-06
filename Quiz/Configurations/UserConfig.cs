
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Entities;

namespace Quiz.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);



            builder.HasData(
                new User { Id = 1, Name = "Setareh Zaman", Email = "setareh@gmail.com", Password = "123" },
                new User { Id = 2, Name = "Narges Dehghan", Email = "Narges@gmail.com", Password = "456" },
                new User { Id = 3, Name = "Sarvenaz Fazli", Email = "Sarvenaz@gmail.com", Password = "789" }
                );
        }
    }
}
