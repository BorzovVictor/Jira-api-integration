using System;
using System.Net.Mime;
using System.Threading;
using AutoMapper;
using Hangfire;
using Jira.Api.Infrastructure;
using Jira.Api.Infrastructure.Extensions;
using Jira.Api.Infrastructure.Options;
using Jira.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jira.Api
{
    public class Startup
    {
    
        private const string corsPermssion = "fully permissive";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMapperConfiguration();
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy(corsPermssion,
                    configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                        .AllowCredentials());
            });
            
            services.AddOptions();
            services.Configure<JCredentials>(Configuration.GetSection("JiraCredentials"));

            services.AddDbContext<DataContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("JiraDataBase")));
            services.AddDbContext<HangFireContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("HangFire")));

            #region HangFire

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("HangFire")));
            services.AddHangfireServer();

            #endregion

            services.AddProjectServices();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region HangFire
            
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            StartJobs();
            #endregion

            app.UseCors(corsPermssion);
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        void StartJobs()
        {
            // https://crontab.guru/
            const string every2ndHour = "0 */2 * * *";
            const string every2ndHourAt10Minute = "10 */2 * * *";
            const string every2ndHourAt20Minute = "20 */2 * * *";
            const string every2ndHourAt30Minute = "30 */2 * * *";
            
            RecurringJob.AddOrUpdate<IJUsersRepository>("users-sync",
                users => users.SyncUsers(CancellationToken.None),
                Cron.Hourly);
            RecurringJob.AddOrUpdate<IJProjectsRepository>("projects-sync",
                project => project.SyncProjects(CancellationToken.None),
                Cron.Hourly);
            RecurringJob.AddOrUpdate<IJBoardRepository>("boards-sync",
                board => board.Sync(CancellationToken.None),
                Cron.Hourly);
            RecurringJob.AddOrUpdate<IJSprintRepository>("sprints-sync",
                sprint => sprint.Sync(CancellationToken.None), every2ndHour
                );
            RecurringJob.AddOrUpdate<IJPriorityRepository>("priorities-sync",
                priority => priority.Sync(CancellationToken.None), every2ndHourAt10Minute
            );            
            RecurringJob.AddOrUpdate<IJStatusCategoryRepository>("status_categories-sync",
                category => category.Sync(CancellationToken.None), every2ndHourAt20Minute
            );            
            RecurringJob.AddOrUpdate<IJStatusRepository>("statuses-sync",
                status => status.Sync(CancellationToken.None), every2ndHourAt30Minute
            );
        }
    }
}