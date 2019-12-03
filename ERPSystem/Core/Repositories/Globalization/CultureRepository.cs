using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.Globalization
{
    public class CultureRepository
    {
        public GlobalCountryRepository GlobalCountryRepository { set; get; }
        public GlobalValueRepository GlobalValueRepository { set; get; }
        public CultureRepository(ERPDatabaseEntities context)
        {
            GlobalValueRepository = new GlobalValueRepository(context);
            GlobalCountryRepository = new GlobalCountryRepository(context);
        }
        
    }
}