using System;
using System.Collections.Generic;

namespace Web_API_2._0.Model;

public partial class Event
{
    public int IdEvent { get; set; }

    public string EventName { get; set; } = null!;

    public string TypeOfEvent { get; set; } = null!;

    public string EventStatus { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public DateTime DateOfEvent { get; set; }

    public string EventManagers { get; set; } = null!;

    public string TypeOfClass { get; set; } = null!;

    public virtual ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();

    public virtual ICollection<EventMaterial> EventMaterials { get; set; } = new List<EventMaterial>();
}
