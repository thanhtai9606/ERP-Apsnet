using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPSystem.Models;
namespace ERPSystem.Core.Repositories.HR
{
    public class EmailAddressRepository : Repository<EmailAddress>
    {
        public EmailAddressRepository(ERPDatabaseEntities context) : base(context) { }
    }
}