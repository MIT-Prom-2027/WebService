using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class historique
{
    public int id_historique { get; set; }

    public int? admin_id { get; set; }

    public DateTime? date_evenement { get; set; }

    public string? description { get; set; }

    public int id_province { get; set; }

    public virtual administration? admin { get; set; }

    public virtual province id_provinceNavigation { get; set; } = null!;
}
