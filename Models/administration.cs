using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class administration
{
    public int id_admin { get; set; }

    public string username { get; set; } = null!;

    public string password { get; set; } = null!;

    public string? description { get; set; }

    public virtual ICollection<historique> historiques { get; set; } = new List<historique>();
}
