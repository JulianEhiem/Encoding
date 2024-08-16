using System;
using System.Collections.Generic;

namespace Encoding.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public string RatingName { get; set; } = null!;

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();
}
