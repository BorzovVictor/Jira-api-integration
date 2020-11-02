using System.Collections;
using System.Collections.Generic;

namespace Jira.Core.Data.Entities
{
    public class JStatusCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string ColorName { get; set; }
        public string Self { get; set; }
        public virtual ICollection<JStatus> Statuses { get; set; }
    }
}