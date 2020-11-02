using System.Collections.Generic;

namespace Jira.Core.Data.Entities
{
    public class JProject
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string LeadKey { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ProjectTypeKey { get; set; }
        public bool Simplified { get; set; }
        public string Style { get; set; }
        public bool IsPrivate { get; set; }
        public virtual ICollection<JBoard> Boards { get; set; }
    }
}