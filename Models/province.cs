using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class province
{
    public int id_province { get; set; }

    public string nom_province { get; set; } = null!;

    public virtual ICollection<centre> centres { get; set; } = new List<centre>();

    public virtual ICollection<historique> historiques { get; set; } = new List<historique>();
}
