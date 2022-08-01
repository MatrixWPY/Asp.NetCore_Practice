using System.Collections.Generic;
using WebApi.Models.Data;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Commands.Interface
{
    public interface IContactInfoCommand
    {
        Result<ContactInfo> QueryByID(long id);

        Result<bool> Add(ContactInfoAddRQ objRQ);

        Result<bool> Edit(ContactInfoEditRQ objRQ);

        Result<bool> DeleteByID(IEnumerable<long> liID);
    }
}
