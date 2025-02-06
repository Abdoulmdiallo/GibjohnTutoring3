using System;
using System.Collections.Generic;

namespace GibjohnTutoring3.Models;

public partial class Leaderboard
{
    public int LeaderboardId { get; set; }

    public int CustomerId { get; set; }

    public int? Level { get; set; }

    public int? Rank { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
