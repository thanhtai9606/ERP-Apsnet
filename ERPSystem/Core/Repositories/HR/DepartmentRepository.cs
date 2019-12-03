using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPSystem.Models;

namespace ERPSystem.Core.Repositories.HR
{
    public class DepartmentRepository : Repository<Department>
    {
        public DepartmentRepository (ERPDatabaseEntities context) : base(context) { }
    }
}