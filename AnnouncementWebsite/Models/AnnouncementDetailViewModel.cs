using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebsite.Models
{
    public class AnnouncementDetailViewModel
    {
        public AnnouncementsViewModel Announcements { get; set; }
        public List<AnnouncementsViewModel> SimilarAnnouncements { get; set; }
        public AnnouncementDetailViewModel(AnnouncementsViewModel announcementsViewModel, List<AnnouncementsViewModel> similarAnnouncements)
        {
            Announcements = announcementsViewModel;
            SimilarAnnouncements = similarAnnouncements;
        }
    }
}
