using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERPSystem.Models;
namespace ERPSystem.Core.Repositories.HR
{
    public class JobRepository:Repository<JobCandidate>
    {
        public JobRepository(ERPDatabaseEntities context) : base(context) { }
    }
}