using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPSystem.Models;

namespace ERPSystem.Core.Repositories.HR
{
    public class EmployeeRepository: Repository<Employee>
    {
        public EmployeeRepository(ERPDatabaseEntities context) : base(context) { }
    }
}