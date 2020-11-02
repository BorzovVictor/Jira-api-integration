using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JStatusCategoryConfiguration: IEntityTypeConfiguration<JStatusCategory>
    {
        public void Configure(EntityTypeBuilder<JStatusCategory> builder)
        {
            builder.ToTable("JiraStatusCategory");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}