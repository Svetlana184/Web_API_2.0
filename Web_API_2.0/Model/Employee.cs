using System;
using System.Collections.Generic;

namespace Web_API_2._0.Model;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string Surname { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? SecondName { get; set; }

    public string Position { get; set; } = null!;

    public string PhoneWork { get; set; } = null!;

    public string? Phone { get; set; }

    public string Cabinet { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int IdDepartment { get; set; }

    public int? IdHelper { get; set; }

    public string? Other { get; set; }

    public DateOnly? BirthDay { get; set; }

    public int? IdBoss { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Calendar> CalendarIdAlternateNavigations { get; set; } = new List<Calendar>();

    public virtual ICollection<Calendar> CalendarIdEmployeeNavigations { get; set; } = new List<Calendar>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual Department IdDepartmentNavigation { get; set; } = null!;
}
