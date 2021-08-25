namespace Demo.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string MorningTask { get; set; }
        public string AfternoonTask { get; set; }
        public string EveningTask { get; set; }
        public bool IsTaskComplete { get; set; }
    }
}
