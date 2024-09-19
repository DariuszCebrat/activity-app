using activity.api.DTO.IdentityDto;
using activity.domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace activity.api.DTO.ActivityDto
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        required public string Title { get; set; }
        required public DateTime Date { get; set; }
        required public string Description { get; set; }
       
        required public string Category { get; set; }
        required public string City { get; set; }
        required public string Venue { get; set; }
        public string HostUserName { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
