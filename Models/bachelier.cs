using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class bachelier
{
    public DateOnly annee { get; set; }

    public string numero_candidat { get; set; } = null!;

    public double moyenne { get; set; }

    public int? id_personne { get; set; }

    public int? id_option { get; set; }

    public int? id_centre { get; set; }

    public int? id_etablissement { get; set; }

    public int? id_mention { get; set; }

    public int id_bachelier { get; set; }

    public virtual centre? id_centreNavigation { get; set; }

    public virtual etablissement? id_etablissementNavigation { get; set; }

    public virtual mention? id_mentionNavigation { get; set; }

    public virtual option? id_optionNavigation { get; set; }

    public virtual personne? id_personneNavigation { get; set; }

    public virtual ICollection<note> notes { get; set; } = new List<note>();
}
