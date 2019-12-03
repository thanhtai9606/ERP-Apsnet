using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.GrantPermissionAdmin
{
    public class GrantPermissionRepository : Repository<GrantPermission>
    {
        public GrantPermissionRepository(ERPDatabaseEntities context) : base(context) { }
    }
}