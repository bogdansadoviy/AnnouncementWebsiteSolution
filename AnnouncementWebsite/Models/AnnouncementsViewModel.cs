using AnnouncementWebsite.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebsite.Models
{
    public class AnnouncementsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; }

        public AnnouncementsViewModel(Announcements announcements)
        {
            Id = announcements.Id;
            Title = announcements.Title;
            Description = announcements.Description;
            DateAdded = announcements.DateAdded;

        }

        public AnnouncementsViewModel()
        {
        }

        public Announcements ToEntity()
        {
            return new Announcements()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                DateAdded = DateAdded == new DateTime() ? DateTime.Now : DateAdded,
            };
        }
    }
}
