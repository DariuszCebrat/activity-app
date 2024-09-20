using System;
using System.ComponentModel.DataAnnotations;

namespace activity.domain.Entities
{
    public class Activity 
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        required public string Title { get; set; }
        required public DateTime Date { get; set; }
        required public string Description { get; set; }
        [MaxLength(100)]
        required public string Category { get; set; }
        [MaxLength(100)]
        required public string City { get; set; }
        [MaxLength(100)]
        required public string Venue { get; set; }
        public bool IsCancelled { get; set; } = false;
        public ICollection<ActivityAttendee> Attendies { get; set; } = new List<ActivityAttendee>();
    }
}
