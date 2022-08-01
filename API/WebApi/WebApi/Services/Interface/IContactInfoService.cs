using System.Collections.Generic;
using WebApi.Models.Data;

namespace WebApi.Services.Interface
{
    public interface IContactInfoService
    {
        ContactInfo Query(long id);

        bool Insert(ContactInfo objContactInfo);

        bool Update(ContactInfo objContactInfo);

        bool Delete(IEnumerable<long> liID);
    }
}
