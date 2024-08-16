using System;
using System.Collections.Generic;

namespace Encoding.Models;

public partial class Problem
{
    public int ProblemId { get; set; }

    public int ProblemNumber { get; set; }

    public string ProblemTitle { get; set; } = null!;

    public string ProblemDescription { get; set; } = null!;

    public int ProblemRatingId { get; set; }

    public virtual Rating ProblemRating { get; set; } = null!;
}
