using System.ComponentModel.DataAnnotations;
using TaskTracker.DomainLayer;

namespace TaskTracker.DataAccess
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        public TaskStatuses Status { get; set; }
        [MaxLength(100)]
        public string AssignedUser { get; set; }
        public int SortIndex { get; set; }
    }
}