namespace Jira.Core.Data.Entities
{
    public class JPriority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string StatusColor { get; set; }
        public string Self { get; set; }
    }
}