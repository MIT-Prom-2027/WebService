using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OutilAdmin.Models;

public partial class Personne
{
    [Key]
    public int IdPersonne { get; set; }

    public string NomPrenom { get; set; } = null!;

    public DateOnly DateNaissance { get; set; }

    public string LieuNaissance { get; set; } = null!;

     public string Sexe { get; set; } = null!; 

    public virtual ICollection<Bachelier> Bacheliers { get; set; } = new List<Bachelier>();
}
