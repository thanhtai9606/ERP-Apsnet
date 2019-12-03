using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.Globalization
{
    public class GlobalValueRepository : Repository<GlobalValue>
    {
        public GlobalValueRepository(ERPDatabaseEntities context) : base(context) { }
    }
}