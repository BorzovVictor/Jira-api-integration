using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JSprintConfiguration: IEntityTypeConfiguration<JSprint>
    {
        public void Configure(EntityTypeBuilder<JSprint> builder)
        {
            builder.ToTable("JiraSprints");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}