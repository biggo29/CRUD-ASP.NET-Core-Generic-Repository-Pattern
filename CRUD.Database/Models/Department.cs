using System;
using System.Collections.Generic;

namespace CRUD.Database.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
