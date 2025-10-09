using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OutilAdmin.Models;

public partial class Option
{
    [Key]
    public int IdOption { get; set; }

    public string? Serie { get; set; }

    public virtual ICollection<Bachelier> Bacheliers { get; set; } = new List<Bachelier>();
}
