using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.HR
{
    public class PasswordRepository : Repository<Password>
    {
        public PasswordRepository(ERPDatabaseEntities context) : base(context) { }
    }
}