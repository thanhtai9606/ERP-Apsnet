using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.BusinessEntities.Phone
{
    public class EntityPhoneRepository : Repository<EntityPhone>
    {
        public EntityPhoneRepository(ERPDatabaseEntities context) : base(context) { }
    }
}