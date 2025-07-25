using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class matiere
{
    public int id_matiere { get; set; }

    public string nom_matiere { get; set; } = null!;

    public virtual ICollection<note> notes { get; set; } = new List<note>();
}
