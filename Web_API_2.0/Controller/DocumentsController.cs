using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web_API_2._0.Model;

namespace Web_API_2._0.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private RoadOfRussiaContext db;
        public DocumentsController(RoadOfRussiaContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IQueryable<DocumentsMaterial> GetMaterials()
        {
            return db.Materials.Select(p => new DocumentsMaterial
            {
                Id = p.IdMaterial,
                Title = p.MaterialName,
                Date_created = p.DateApproval.Date,
                Date_updated = p.DateChanges,
                Has_comments = p.Comments == 0 ? false : true,
            }).AsQueryable<DocumentsMaterial>();
        }
    }
}
