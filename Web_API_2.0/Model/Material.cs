using System;
using System.Collections.Generic;

namespace Web_API_2._0.Model;

public partial class Material
{
    public int IdMaterial { get; set; }

    public string MaterialName { get; set; } = null!;

    public DateTime DateApproval { get; set; }

    public DateTime DateChanges { get; set; }

    public string Status { get; set; } = null!;

    public string TypeOfMaterial { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int Comments { get; set; }

    public virtual ICollection<Comment> CommentsNavigation { get; set; } = new List<Comment>();

    public virtual ICollection<EventMaterial> EventMaterials { get; set; } = new List<EventMaterial>();
}
