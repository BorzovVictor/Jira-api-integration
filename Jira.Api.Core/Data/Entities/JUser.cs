using System.Collections;
using System.Collections.Generic;

namespace Jira.Core.Data.Entities
{
    public class JUser
    {
        public string Key { get; set; }
        public string Self { get; set; }
        public string AccountId { get; set; }
        public string AccountType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string TimeZone { get; set; }
        public string Locale { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
    }
}