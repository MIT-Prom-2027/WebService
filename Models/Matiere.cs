using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class Matiere
{
    public int IdMatiere { get; set; }

    public string Matiere1 { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
