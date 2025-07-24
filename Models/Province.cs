using System;
using System.Collections.Generic;

namespace UnivManager.Models;

public partial class Province
{
    public int IdProvince { get; set; }

    public string Province1 { get; set; } = null!;

    public virtual ICollection<Centre> Centres { get; set; } = new List<Centre>();
}
