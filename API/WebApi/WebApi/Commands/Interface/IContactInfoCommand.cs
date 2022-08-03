using System.Collections.Generic;
using WebApi.Models.Data;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Commands.Interface
{
    public interface IContactInfoCommand
    {
        ApiResult<ContactInfo> QueryByID(long id);

        ApiResult<PageData<IEnumerable<ContactInfo>>> QueryByCondition(ContactInfoQueryRQ objRQ);

        ApiResult<ContactInfo> Add(ContactInfoAddRQ objRQ);

        ApiResult<ContactInfo> Edit(ContactInfoEditRQ objRQ);

        ApiResult<ContactInfo> EditPartial(ContactInfoEditPartialRQ objRQ);

        ApiResult<bool> DeleteByID(IEnumerable<long> liID);
    }
}
