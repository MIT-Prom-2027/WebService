using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class Note
{
    public int IdNote { get; set; }

    public double Note1 { get; set; }

    public bool? EstOptionnel { get; set; }

    public int? IdMatiere { get; set; }

    public int? IdBachelier { get; set; }

    public virtual Bachelier? IdBachelierNavigation { get; set; }

    public virtual Matiere? IdMatiereNavigation { get; set; }
}
