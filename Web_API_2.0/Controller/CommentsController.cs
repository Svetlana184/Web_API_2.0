using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Web_API_2._0.Model;

namespace Web_API_2._0.Controller
{
    [ApiController]
    
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private RoadOfRussiaContext db;
        public CommentsController(RoadOfRussiaContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("api/v1/Document/{id:int}/[controller]")]
        public IQueryable<DocumentsComment> GetComments(int id) 
        {
            var comments = from c in db.Comments
                           join e in db.Employees on c.AuthorOfComment equals e.IdEmployee
                           select new DocumentsComment
                           {
                               Id = c.IdComment,
                               Document_id = c.IdMaterial,
                               Text = c.CommentText,
                               Date_created = c.DateCreated,
                               Date_updated = c.DateUpdated,
                               NameAuthor = e.Surname + " " + e.FirstName,
                               Position = e.Position
                           };
            return comments.Where(p => p.Document_id == id).AsQueryable<DocumentsComment>();
        }
    
        
        
    } 
}
