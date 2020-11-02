using Jira.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Core.Data.TypeConfigurations
{
    public class JBoardConfiguration : IEntityTypeConfiguration<JBoard>
    {
        public void Configure(EntityTypeBuilder<JBoard> builder)
        {
            builder.ToTable("JiraBoards");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}