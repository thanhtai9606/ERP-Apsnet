using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.HR
{
    public class EmployeeDepartmentRepository : Repository<EmployeeDepartmentHistory>
    {
        public EmployeeDepartmentRepository (ERPDatabaseEntities context) : base(context) { }
    }
}