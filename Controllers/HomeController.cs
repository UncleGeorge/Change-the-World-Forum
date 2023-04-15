using ITE5331FinalProject.Data;
using ITE5331FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ITE5331FinalProject.Controllers
{
    public class HomeController : Controller
    {

        private ForumContext _forumContext { get; set; }

        public HomeController(ForumContext forumContext)
        {
            _forumContext = forumContext;
        }

        // open home page, list all articles
        public IActionResult Index()
        {
            var articles = _forumContext.Articles
                .OrderByDescending(a => a.PublishTime)
                .ToList();
            return View(articles);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // open chat page, send current user to the page
        public IActionResult Chat()
        {
            var author = User.Identity.Name ?? "Guest";
            ViewData["Author"] = author;
            return View();
        }

        // search function, get all articles with the search string in title and text
        public IActionResult Search(string searchString)
        {
            var articles = from a in _forumContext.Articles
                           where a.Title.Contains(searchString) || a.Text.Contains(searchString)
                           select a;

            return View("Index", articles.ToList());
        }
    }
}