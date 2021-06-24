using AnnouncementWebsite.DataAccess;
using AnnouncementWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AnnouncementWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        private bool AnnouncementsExists(int id)
        {
            return _context.Announcements.Any(e => e.Id == id);
        }
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var announcements = await _context.Announcements.ToListAsync();
            var announcementsViewModels = announcements.Select(_ => new AnnouncementsViewModel(_)).ToList();
     

            return View(announcementsViewModels);
        }

        // GET: AdminPanel/Create
        public async Task<IActionResult> Create()
        {
            var vm = new AnnouncementsViewModel();
            vm.DateAdded = DateTime.Now;

            return View(vm);
        }

        // GET: AdminPanel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcement = await _context.Announcements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcement == null)
            {
                return NotFound();
            }

            var titleWords = announcement.Title
                .Split(" ")
                .Select(_ => _.Trim(new char[] { ',', '.' }))
                .ToList();
            var descriptionWords = announcement.Description
                .Split(" ")
                .Select(_ => _.Trim(new char[] { ',', '.' }))
                .ToList();

            var similarAnnoncements = _context.Announcements
                .AsEnumerable()
                .Where(_ => _.Id != announcement.Id)
                .Where(_ => titleWords.Any(w => _.Title.Contains(w)) || descriptionWords.Any(w => _.Description.Contains(w)))
                .Take(3);

            var vm = new AnnouncementDetailViewModel(
                new AnnouncementsViewModel(announcement),
                similarAnnoncements.Select(_ => new AnnouncementsViewModel(_)).ToList());

            return View(vm);
        }

        // GET: AdminPanel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.Announcements.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            var vm = new AnnouncementsViewModel(news);

            return View(vm);
        }

        // POST: AdminPanel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnnouncementsViewModel announcements)
        {
            if (id != announcements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcements.ToEntity());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementsExists(announcements.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(announcements);
        }

        // POST: AdminPanel/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.Announcements.FindAsync(id);
            _context.Announcements.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
