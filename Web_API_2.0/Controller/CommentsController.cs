using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IQueryable GetComments(int id) 
        {
            var query= from c in db.Comments
                           join e in db.Employees on c.AuthorOfComment equals e.IdEmployee
                           where  id==c.IdMaterial                    
                           select new 
                           {
                               Id = c.IdComment,
                               Document_id = c.IdMaterial,
                               Text = c.CommentText,
                               Date_created = c.DateCreated,
                               Date_updated = c.DateUpdated,
                               Author =db.Employees.Select(p=>new {name=p.Surname + " " + p.FirstName, position = p.Position}).FirstOrDefault(d=>e.IdEmployee==c.AuthorOfComment)!
                           };
            if  (query!=null) return query.AsQueryable();
            return ;
        }
    
        
        
    } 
}
