using ERPSystem.Models;

namespace ERPSystem.Core.Repositories.BusinessEntities
{
    public class EntityRepository:Repository<BusinessEntity>
    {
        public EntityRepository (ERPDatabaseEntities context) : base(context) { }
    }
}