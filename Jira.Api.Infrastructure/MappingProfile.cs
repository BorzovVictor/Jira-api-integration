using System;
using Atlassian.Jira;
using AutoMapper;
using Jira.Api.Infrastructure.Extensions;
using Jira.Api.Infrastructure.Models;
using Jira.Core.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace Jira.Api.Infrastructure
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<JiraUserFull, JUser>()
                .ForMember(d => d.Self, opt => opt.MapFrom(s => s.Self.OriginalString))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.EmailAddress))
                .ForMember(d => d.AvatarUrl, opt => opt.MapFrom(s => s.AvatarUrls.The48X48.OriginalString))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.Active))
                .ReverseMap();

            CreateMap<JProjectFull, JProject>()
                .ForMember(d => d.LeadKey, opt => opt.MapFrom(s => s.Lead.Key))
                .ForMember(d => d.Icon, opt => opt.MapFrom(s => s.AvatarUrls.The48X48.OriginalString))
                .ForMember(d => d.Boards, opt => opt.Ignore());

            CreateMap<JSprintItem, JSprint>()
                .ForMember(d => d.Self, opt => opt.MapFrom(s => s.Self.OriginalString))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => GetDateTime(s.StartDate)))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => GetDateTime(s.EndDate)))
                .ForMember(d => d.CompleteDate, opt => opt.MapFrom(s => GetDateTime(s.CompleteDate)))
                .ReverseMap();
            
            CreateMap<JBoardItem, JBoard>()
                .ForMember(d => d.Self, opt => opt.MapFrom(s => s.Self.OriginalString))
                .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Location.ProjectId))
                .ForMember(d => d.Project, opt => opt.Ignore());

            CreateMap<JStatusItem, JStatus>()
                .ForMember(d => d.Self, opt => opt.MapFrom(s => s.Self.OriginalString))
                .ForMember(d => d.IconUrl, opt => opt.MapFrom(s => s.IconUrl.OriginalString))
                .ForMember(d => d.JStatusCategoryId, opt => opt.MapFrom(s => s.StatusCategory.Id));

            CreateMap<JStatusCategoryItem, JStatusCategory>()
                .ForMember(x => x.Statuses, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Author, JUser>()
                .ForMember(d => d.Self, opt => opt.MapFrom(s => s.Self.OriginalString))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.Active))
                .ForMember(d => d.AvatarUrl, opt => opt.MapFrom(s => s.AvatarUrls.The48X48.OriginalString))
                .ForMember(d => d.Locale, opt => opt.Ignore())
                .ForMember(d => d.Email, opt => opt.Ignore());

            CreateMap<JIssueCommentItem, JIssueComment>()
                .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author));

            CreateMap<JIssueWorklogItem, JIssueWorklog>()
                .ForMember(d => d.Created, opt => opt.MapFrom(s => ConvertToDateTime(s.Created)))
                .ForMember(d => d.Updated, opt => opt.MapFrom(s => ConvertToDateTime(s.Updated)))
                .ForMember(d => d.Started, opt => opt.MapFrom(s => ConvertToDateTime(s.Started)))
                .ForMember(d => d.Self, opt => opt.MapFrom(s => s.Self.OriginalString));
        }

        private static DateTime? GetDateTime(DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset?.DateTime;
        }
        
        private static DateTime? ConvertToDateTime(string value)
        {
            try {
                return Convert.ToDateTime(value);
            }
            catch (FormatException) {
                return null;
            }
        }
    }
}