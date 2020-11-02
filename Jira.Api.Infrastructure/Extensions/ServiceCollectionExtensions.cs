using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Jira.Api.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddTransient<IJUsersRepository, JUsersRepository>();
            services.AddTransient<IJUsersService, JUsersService>();
            
            services.AddTransient<IJProjectService, JProjectService>();
            services.AddTransient<IJProjectsRepository, JProjectsRepository>();
            
            services.AddTransient<IJSprintService, JSprintService>();
            services.AddTransient<IJSprintRepository, JSprintRepository>();
            
            services.AddTransient<IJBoardService, JBoardService>();
            services.AddTransient<IJBoardRepository, JBoardRepository>();      
            
            services.AddTransient<IJIssueTypeService, JIssueTypeService>();
            services.AddTransient<IJIssueTypeRepository, JIssueTypeRepository>();      
            
            services.AddTransient<IJPriorityService, JPriorityService>();
            services.AddTransient<IJPriorityRepository, JPriorityRepository>();  
            
            services.AddTransient<IJStatusCategoryService, JStatusCategoryService>();
            services.AddTransient<IJStatusCategoryRepository, JStatusCategoryRepository>();   
            
            services.AddTransient<IJStatusService, JStatusService>();
            services.AddTransient<IJStatusRepository, JStatusRepository>();
            
            services.AddTransient<IJIssueCommentService, JIssueCommentService>();
            
            services.AddTransient<IJIssueWorklogService, JIssueWorklogService>();
            
            return services;
        }

        public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            return services;
        }
    }
}