using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JPriorityConfiguration: IEntityTypeConfiguration<JPriority>
    {
        public void Configure(EntityTypeBuilder<JPriority> builder)
        {
            builder.ToTable("JiraPriority");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}