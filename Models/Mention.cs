using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class Mention
{
    public int IdMention { get; set; }

    public string Mention1 { get; set; } = null!;

    public int? Min { get; set; }

    public int? Max { get; set; }

    public virtual ICollection<Bachelier> Bacheliers { get; set; } = new List<Bachelier>();
}
