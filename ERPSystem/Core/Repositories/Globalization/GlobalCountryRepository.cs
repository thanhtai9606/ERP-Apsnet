using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.Globalization
{
    public class GlobalCountryRepository : Repository<GlobalCountry>
    {
        public GlobalCountryRepository(ERPDatabaseEntities context) : base(context) { }
    }
}