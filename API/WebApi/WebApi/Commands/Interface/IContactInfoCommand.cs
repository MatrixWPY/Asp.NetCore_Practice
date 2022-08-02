using System.Collections.Generic;
using WebApi.Models.Data;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Commands.Interface
{
    public interface IContactInfoCommand
    {
        Result<ContactInfo> QueryByID(long id);

        Result<IEnumerable<ContactInfo>> QueryByCondition(ContactInfoQueryRQ objRQ);

        Result<ContactInfo> Add(ContactInfoAddRQ objRQ);

        Result<ContactInfo> Edit(ContactInfoEditRQ objRQ);

        Result<ContactInfo> EditPartial(ContactInfoEditPartialRQ objRQ);

        Result<bool> DeleteByID(IEnumerable<long> liID);
    }
}
