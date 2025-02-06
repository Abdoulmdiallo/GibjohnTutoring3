using System;
using System.Collections.Generic;

namespace GibjohnTutoring3.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? CustomerId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? Created { get; set; }

    public virtual Customer? Customer { get; set; }
}
