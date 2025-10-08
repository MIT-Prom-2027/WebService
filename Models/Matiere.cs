using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OutilAdmin.Models;

public partial class Matiere
{
    [Key]
    public int IdMatiere { get; set; }

    public string NomMatiere { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
