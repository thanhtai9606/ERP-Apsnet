using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.BusinessEntities.AddressRepo
{
    public class BusinessEntityAddressRepository: Repository<BusinessEntityAddress>
    {
        public BusinessEntityAddressRepository(ERPDatabaseEntities context) : base (context){ }
    }
}