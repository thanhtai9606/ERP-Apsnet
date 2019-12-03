using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.BusinessEntities.AddressRepo
{
    public class AddressRepository : Repository<Address>
    {
        public AddressRepository (ERPDatabaseEntities context): base(context) { }
    }
}