using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class etablissement
{
    public int id_etablissement { get; set; }

    public string nom_etablissement { get; set; } = null!;

    public virtual ICollection<bachelier> bacheliers { get; set; } = new List<bachelier>();
}
