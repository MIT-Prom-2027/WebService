using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OutilAdmin.Models;

public partial class Note
{
    [Key]
    public int IdNote { get; set; }

    public double ValeurNote { get; set; }

    public bool? EstOptionnel { get; set; }

    public int? IdMatiere { get; set; }

    public int? IdBachelier { get; set; }

    public virtual Bachelier? IdBachelierNavigation { get; set; }

    public virtual Matiere? IdMatiereNavigation { get; set; }
}
