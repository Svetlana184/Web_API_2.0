using System;
using System.Collections.Generic;
using Web_API_2._0.Model;

namespace Web_API_2._0;

public partial class Department
{
    public int IdDepartment { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Description { get; set; }

    public int? IdEmployee { get; set; }

    public int? IdDepartmentParent { get; set; }

    public string? ДорогиРоссии { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Employee? IdEmployeeNavigation { get; set; }
}
