using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class Bachelier
{
    public DateOnly Annee { get; set; }

    public string NumeroCandidat { get; set; } = null!;

    public double Moyenne { get; set; }

    public int? IdPersonne { get; set; }

    public int? IdOption { get; set; }

    public int? IdCentre { get; set; }

    public int? IdEtablissement { get; set; }

    public int? IdMention { get; set; }

    public int IdBachelier { get; set; }

    public virtual Centre? IdCentreNavigation { get; set; }

    public virtual Etablissement? IdEtablissementNavigation { get; set; }

    public virtual Mention? IdMentionNavigation { get; set; }

    public virtual Option? IdOptionNavigation { get; set; }

    public virtual Personne? IdPersonneNavigation { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
