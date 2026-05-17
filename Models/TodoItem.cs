namespace Activity5_AppDev.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public TimeOnly TimeDeadline { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}