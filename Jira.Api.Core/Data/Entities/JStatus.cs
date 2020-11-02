namespace Jira.Core.Data.Entities
{
    public class JStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string Self { get; set; }
        public virtual JStatusCategory StatusCategory { get; set; }
        public int JStatusCategoryId { get; set; }
    }
}