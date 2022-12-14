﻿using System;
using System.Collections.Generic;

namespace sp_first_approach.Models
{
    public partial class Department
    {
        public Department()
        {
            UsersDetails = new HashSet<UsersDetail>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;

        public virtual ICollection<UsersDetail> UsersDetails { get; set; }
    }
}
