using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace OutilAdmin.Models;

public partial class Administration
{
    [Key]
    public int IdAdmin { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Historique> Historiques { get; set; } = new List<Historique>();
}
