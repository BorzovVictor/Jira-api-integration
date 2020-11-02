using Jira.Core.Data.Entities;
using Jira.Core.Data.TypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Jira.Core.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }
        public DbSet<JUser> JUsers { get; set; }
        public DbSet<JProject> JProjects { get; set; }
        public DbSet<JBoard> JBoards { get; set; }
        public DbSet<JSprint> JSprints { get; set; }
        public DbSet<JIssueType> JIssueTypes { get; set; }
        public DbSet<JPriority> JPriorities { get; set; }
        public DbSet<JStatusCategory> JStatusCategories { get; set; }
        public DbSet<JStatus> JStatuses { get; set; }
        public DbSet<JIssueWorklog> JWorklogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new JUserConfiguration());
            builder.ApplyConfiguration(new JProjectConfiguration());
            builder.ApplyConfiguration(new JSprintConfiguration());
            builder.ApplyConfiguration(new JBoardConfiguration());
            builder.ApplyConfiguration(new JIssueTypeConfiguration());
            builder.ApplyConfiguration(new JPriorityConfiguration());
            builder.ApplyConfiguration(new JStatusCategoryConfiguration());
            builder.ApplyConfiguration(new JStatusConfiguration());
            builder.ApplyConfiguration(new JIssueWorklogConfiguration());
        }
    }
}