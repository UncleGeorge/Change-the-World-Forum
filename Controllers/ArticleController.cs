using ITE5331FinalProject.Data;
using ITE5331FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITE5331FinalProject.Controllers
{
    public class ArticleController : Controller
    {

        private ForumContext _forumContext { get; set; }
        public ArticleController(ForumContext forumContext)
        {
            _forumContext = forumContext;
        }

        // Page to show article and its comments
        public IActionResult Details(int id)
        {
            var article = _forumContext.Articles
                .Include(a => a.Comments)
                .FirstOrDefault(a => a.ArticleId == id);
            if (article == null)
            {
                return NotFound("Cannot find the article");
            }

            article.NumViews++;
            _forumContext.SaveChanges();

            return View(article);
        }

        [HttpPost]
        public async Task<ActionResult> AddArticle(String Title, String Text)
        {

            Article newArticle = new Article
            {
                Title = Title,
                Author = User.Identity.Name ?? "Guest",
                Text = Text,
                PublishTime = DateTime.Now,
                NumViews = 0,
                NumLikes = 0,
                Tag = Tag.Normal,
                Comments = new List<Comment>()
            };

            _forumContext.Articles.Add(newArticle);
            await _forumContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(String Text, int ArticleId)
        {

            Comment newComment = new Comment
            {
                ArticleId = ArticleId,
                Author = User.Identity.Name ?? "Guest",
                Text = Text,
                PublishTime = DateTime.Now,
                NumLikes = 0
            };

            _forumContext.Comments.Add(newComment);
            await _forumContext.SaveChangesAsync();

            return RedirectToAction("Details", "Article", new { id = ArticleId });
        }

        public IActionResult EditComment(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var comment = _forumContext.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost]
        public IActionResult EditComment(int CommentId, string Text)
        {
            var comment = _forumContext.Comments.Find(CommentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Text = Text;
            _forumContext.Update(comment);
            _forumContext.SaveChanges();

            return RedirectToAction("Details", new { id = comment.ArticleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int CommentId)
        {
            var comment = await _forumContext.Comments.FindAsync(CommentId);
            if (comment == null)
            {
                return NotFound();
            }

            _forumContext.Comments.Remove(comment);
            await _forumContext.SaveChangesAsync();

            return RedirectToAction("Details", new { id = comment.ArticleId });
        }

        [HttpPost]
        public IActionResult Upvote(int ArticleId)
        {
            var article = _forumContext.Articles.Find(ArticleId);
            if (article == null)
            {
                return NotFound();
            }

            article.NumLikes += 1;
            _forumContext.Update(article);
            _forumContext.SaveChanges();

            return RedirectToAction("Details", new { id = article.ArticleId });
        }

        [HttpPost]
        public IActionResult UpvoteComment(int CommentId)
        {
            var comment = _forumContext.Comments.Find(CommentId);
            if (comment == null)
            {
                return NotFound();
            }

            comment.NumLikes += 1;
            _forumContext.Update(comment);
            _forumContext.SaveChanges();

            return RedirectToAction("Details", new { id = comment.ArticleId });
        }

        [HttpPost]
        public IActionResult UpdateTag(int ArticleId, Tag SelectedTag)
        {
            var article = _forumContext.Articles.FirstOrDefault(a => a.ArticleId == ArticleId);

            if (article != null)
            {
                article.Tag = SelectedTag;
                _forumContext.Update(article);
                _forumContext.SaveChanges();
            }

            return RedirectToAction("Details", new { id = ArticleId });
        }
    }
}
