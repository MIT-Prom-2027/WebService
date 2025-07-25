using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class mention
{
    public int id_mention { get; set; }

    public string nom_mention { get; set; } = null!;

    public int? min { get; set; }

    public int? max { get; set; }

    public virtual ICollection<bachelier> bacheliers { get; set; } = new List<bachelier>();
}
