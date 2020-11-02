using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JStatusConfiguration: IEntityTypeConfiguration<JStatus>
    {
        public void Configure(EntityTypeBuilder<JStatus> builder)
        {
            builder.ToTable("JiraStatuses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}