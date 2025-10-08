using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OutilAdmin.Models;

public partial class Province
{
    [Key]
    public int IdProvince { get; set; }

    public string NomProvince { get; set; } = null!;

    public virtual ICollection<Centre> Centres { get; set; } = new List<Centre>();

    public virtual ICollection<Historique> Historiques { get; set; } = new List<Historique>();
}
