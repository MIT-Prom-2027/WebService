using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class option
{
    public int id_option { get; set; }

    public string? serie { get; set; }

    public virtual ICollection<bachelier> bacheliers { get; set; } = new List<bachelier>();
}
