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
                return db.Comments.Select(p => new DocumentsComment
                {
                    Id = p.IdComment,
                    Document_id = p.IdMaterial,
                    Text = p.CommentText,
                    Date_created = p.DateCreated,
                    Date_updated = p.DateUpdated
                }).Where(p=>p.Document_id==id).AsQueryable<DocumentsComment>()&&
                db.Employees.Select(p=> );
        }
    
        
        
    } 
}
