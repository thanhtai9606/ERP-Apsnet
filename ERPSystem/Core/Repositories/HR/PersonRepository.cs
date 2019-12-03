using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSystem.Core.Repositories.HR
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(ERPDatabaseEntities context) : base(context) { }
    }
}