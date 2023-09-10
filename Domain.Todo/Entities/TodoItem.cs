namespace TodoWebService.Models.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string ?Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset UpdatedTime { get; set; }
        public string ?UserId { get; set; }
        public virtual AppUser ?User { get; set; }

    }
}