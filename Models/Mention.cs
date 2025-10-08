using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OutilAdmin.Models;

public partial class Mention
{
    [Key]
    public int IdMention { get; set; }

    public string NomMention { get; set; } = null!;

    public int? Min { get; set; }

    public int? Max { get; set; }

    public virtual ICollection<Bachelier> Bacheliers { get; set; } = new List<Bachelier>();
}
