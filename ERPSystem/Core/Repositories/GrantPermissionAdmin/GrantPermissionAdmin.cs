using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.GrantPermissionAdmin
{
    public class GrantPermissionAdmin
    {

        public BusinessRepository BusinessRepository { set; get; }
        public GrantPermissionRepository GrantPermissionRepository { set; get; }
        public PermissionRepository PermissionRepository { set; get; }
        public GrantPermissionAdmin(ERPDatabaseEntities _context)
        {
            BusinessRepository = new BusinessRepository(_context);
            GrantPermissionRepository = new GrantPermissionRepository(_context);
            PermissionRepository = new PermissionRepository(_context);

        }
    }
}