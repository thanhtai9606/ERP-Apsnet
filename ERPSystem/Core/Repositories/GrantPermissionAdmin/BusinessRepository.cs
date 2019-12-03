using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.GrantPermissionAdmin
{
    public class BusinessRepository : Repository<Business>
    {
        public BusinessRepository(ERPDatabaseEntities context) : base(context) { }
    }
}