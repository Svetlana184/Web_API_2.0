using System;
using System.Collections.Generic;

namespace Web_API_2._0.Model;

public partial class Comment
{
    public int IdComment { get; set; }

    public int IdMaterial { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public int AuthorOfComment { get; set; }

    public virtual Employee AuthorOfCommentNavigation { get; set; } = null!;

    public virtual Material IdMaterialNavigation { get; set; } = null!;
}
