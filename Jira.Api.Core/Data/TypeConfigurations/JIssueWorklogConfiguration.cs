using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JIssueWorklogConfiguration: IEntityTypeConfiguration<JIssueWorklog>
    {
        public void Configure(EntityTypeBuilder<JIssueWorklog> builder)
        {
            builder.ToTable("JiraIssueWorklogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}