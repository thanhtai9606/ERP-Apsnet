using ERPSystem.Models;

namespace ERPSystem.Core.Repositories.BusinessEntities.Phone
{
    public class BusinessEntityPhoneRepository:Repository<BusinessEntityPhone>
    {
        public BusinessEntityPhoneRepository(ERPDatabaseEntities context) : base (context){ }
    }
}