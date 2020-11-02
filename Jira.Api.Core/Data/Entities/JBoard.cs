namespace Jira.Core.Data.Entities
{
    public class JBoard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Self { get; set; }
        public virtual JProject Project { get; set; }
        public int ProjectId { get; set; }
    }
}