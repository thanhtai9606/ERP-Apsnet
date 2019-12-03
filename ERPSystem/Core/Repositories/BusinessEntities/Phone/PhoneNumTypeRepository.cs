using ERPSystem.Models;

namespace ERPSystem.Core.Repositories.BusinessEntities.Phone
{
    public class PhoneNumTypeRepository : Repository<PhoneNumberType>
    {
        public PhoneNumTypeRepository(ERPDatabaseEntities  context) : base(context) { }
    }
}