namespace Jira.Core.Data.Entities
{
    public class JIssueType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Self { get; set; }
        public bool SubTask { get; set; }
        public int? AvatarId { get; set; }
    }
}