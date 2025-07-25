using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class personne
{
    public int id_personne { get; set; }

    public string nom_prenom { get; set; } = null!;

    public DateOnly date_naissance { get; set; }

    public string lieu_naissance { get; set; } = null!;

    public virtual ICollection<bachelier> bacheliers { get; set; } = new List<bachelier>();
}
