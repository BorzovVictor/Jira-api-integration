## Jira integration with partial sync
in the project plans, integrate with Jira, then synchronize some of the data for the possibility of offline work and automate the routine.
Project uses HangFire for scheduled synchronization

in the plans to add integration with upwork here

## add entity
1 json model
2 entity
 - entity type configuration
    - add configuration to DataContext
 - add DbSet<> to DataContext  
 - add migration
 - map configuration
3 service 
4 repository
5 register service
6 schedule sync


## sync method
- get records from jira
- foreach record - get item from db
  --if item not found 
  --- create new item 
  ---else update item
- save to database



- dotnet ef migrations add JIssueWorklogAdded -p Jira.Api.Core -s Jira.Api -c DataContext
- dotnet ef migrations remove -p Jira.Api.Core -s Jira.Api -c DataContext
