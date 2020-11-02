﻿// <auto-generated />
using System;
using Jira.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jira.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191028105522_JStatusesAdded")]
    partial class JStatusesAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Jira.Core.Data.Entities.JBoard", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.Property<int>("ProjectId");

                    b.Property<string>("Self");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("JiraBoards");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JIssueType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int?>("AvatarId");

                    b.Property<string>("Description");

                    b.Property<string>("IconUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Self");

                    b.Property<bool>("SubTask");

                    b.HasKey("Id");

                    b.ToTable("JiraIssueTypes");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JPriority", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description");

                    b.Property<string>("IconUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Self");

                    b.Property<string>("StatusColor");

                    b.HasKey("Id");

                    b.ToTable("JiraPriority");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JProject", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsPrivate");

                    b.Property<string>("Key");

                    b.Property<string>("LeadKey");

                    b.Property<string>("Name");

                    b.Property<string>("ProjectTypeKey");

                    b.Property<bool>("Simplified");

                    b.Property<string>("Style");

                    b.HasKey("Id");

                    b.ToTable("JiraProjects");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JSprint", b =>
                {
                    b.Property<long>("Id");

                    b.Property<DateTime?>("CompleteDate");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Goal");

                    b.Property<string>("Name");

                    b.Property<int>("OriginBoardId");

                    b.Property<string>("Self");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("State");

                    b.HasKey("Id");

                    b.ToTable("JiraSprints");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JStatus", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Description");

                    b.Property<string>("IconUrl");

                    b.Property<int>("JStatusCategoryId");

                    b.Property<string>("Name");

                    b.Property<string>("Self");

                    b.HasKey("Id");

                    b.HasIndex("JStatusCategoryId");

                    b.ToTable("JiraStatuses");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JStatusCategory", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("ColorName");

                    b.Property<string>("Key");

                    b.Property<string>("Name");

                    b.Property<string>("Self");

                    b.HasKey("Id");

                    b.ToTable("JiraStatusCategory");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JUser", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountId");

                    b.Property<string>("AccountType");

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Locale");

                    b.Property<string>("Name");

                    b.Property<string>("Self");

                    b.Property<string>("TimeZone");

                    b.HasKey("Key");

                    b.ToTable("JiraUsers");
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JBoard", b =>
                {
                    b.HasOne("Jira.Core.Data.Entities.JProject", "Project")
                        .WithMany("Boards")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jira.Core.Data.Entities.JStatus", b =>
                {
                    b.HasOne("Jira.Core.Data.Entities.JStatusCategory", "StatusCategory")
                        .WithMany("Statuses")
                        .HasForeignKey("JStatusCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
