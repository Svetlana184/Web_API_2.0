using System;
using System.Collections.Generic;

namespace Web_API_2._0.Model;

public partial class Calendar
{
    public int IdCalendar { get; set; }

    public string TypeOfEvent { get; set; } = null!;

    public int? IdEmployee { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly DateFinish { get; set; }

    public int? IdEvent { get; set; }

    public string? TypeOfAbsense { get; set; }

    public int? IdAlternate { get; set; }

    public virtual Employee? IdAlternateNavigation { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual Event? IdEventNavigation { get; set; }
}
