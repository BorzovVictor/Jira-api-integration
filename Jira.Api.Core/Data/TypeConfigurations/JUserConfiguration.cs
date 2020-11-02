using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JUserConfiguration: IEntityTypeConfiguration<JUser>
    {
        public void Configure(EntityTypeBuilder<JUser> builder)
        {
            builder.ToTable("JiraUsers");
            builder.HasKey(x => x.Key);
        }
    }
}