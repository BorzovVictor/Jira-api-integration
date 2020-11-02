using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JIssueTypeConfiguration: IEntityTypeConfiguration<JIssueType>
    {
        public void Configure(EntityTypeBuilder<JIssueType> builder)
        {
            builder.ToTable("JiraIssueTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}