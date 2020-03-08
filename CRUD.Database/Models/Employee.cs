using System;
using System.Collections.Generic;

namespace CRUD.Database.Models
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int EmpAge { get; set; }
        public string EmpGender { get; set; }
        public int DeptId { get; set; }
        public string EmpEmail { get; set; }
        public byte[] EmpPhoto { get; set; }

        public virtual Department Dept { get; set; }
    }
}
