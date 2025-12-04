using System.ComponentModel.DataAnnotations;

namespace TaskManagerApp.Models
{
    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    }
}
