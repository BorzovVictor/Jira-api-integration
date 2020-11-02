using System;

namespace Jira.Core.Data.Entities
{
    public class JSprint
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int OriginBoardId { get; set; }
        public string Goal { get; set; }
        public string Self { get; set; }
    }
}