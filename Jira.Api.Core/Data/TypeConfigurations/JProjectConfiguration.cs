using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JProjectConfiguration: IEntityTypeConfiguration<JProject>
    {
        public void Configure(EntityTypeBuilder<JProject> builder)
        {
            builder.ToTable("JiraProjects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}