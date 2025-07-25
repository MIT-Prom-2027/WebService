using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class note
{
    public int id_note { get; set; }

    public double valeur_note { get; set; }

    public bool? est_optionnel { get; set; }

    public int? id_matiere { get; set; }

    public int? id_bachelier { get; set; }

    public virtual bachelier? id_bachelierNavigation { get; set; }

    public virtual matiere? id_matiereNavigation { get; set; }
}
