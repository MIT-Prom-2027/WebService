using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class Centre
{
    public int IdCentre { get; set; }

    public string Centre1 { get; set; } = null!;

    public int? IdProvince { get; set; }

    public virtual ICollection<Bachelier> Bacheliers { get; set; } = new List<Bachelier>();

    public virtual Province? IdProvinceNavigation { get; set; }
}
