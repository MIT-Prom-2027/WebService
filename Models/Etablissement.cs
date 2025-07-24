using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class Etablissement
{
    public int IdEtablissement { get; set; }

    public string Etablissement1 { get; set; } = null!;

    public virtual ICollection<Bachelier> Bacheliers { get; set; } = new List<Bachelier>();
}
