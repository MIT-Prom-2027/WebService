using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class centre
{
    public int id_centre { get; set; }

    public string nom_centre { get; set; } = null!;

    public int? id_province { get; set; }

    public virtual ICollection<bachelier> bacheliers { get; set; } = new List<bachelier>();

    public virtual province? id_provinceNavigation { get; set; }
}
