using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Commands.Interface;
using WebApi.Filters;
using WebApi.Models.Data;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ValidRequest]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoCommand _contactInfoCommand;

        public ContactInfoController(IContactInfoCommand contactInfoCommand)
        {
            _contactInfoCommand = contactInfoCommand;
        }

        [HttpPost]
        public ApiResult<ContactInfo> GetContactInfo(IdRQ objRQ)
        {
            return _contactInfoCommand.QueryByID(objRQ.ID ?? 0);
        }

        [HttpPost]
        public ApiResult<PageData<IEnumerable<ContactInfo>>> ListContactInfo(ContactInfoQueryRQ objRQ)
        {
            return _contactInfoCommand.QueryByCondition(objRQ);
        }

        [HttpPost]
        public ApiResult<ContactInfo> AddContactInfo(ContactInfoAddRQ objRQ)
        {
            return _contactInfoCommand.Add(objRQ);
        }

        [HttpPost]
        public ApiResult<ContactInfo> EditContactInfo(ContactInfoEditRQ objRQ)
        {
            return _contactInfoCommand.Edit(objRQ);
        }

        [HttpPost]
        public ApiResult<ContactInfo> EditPartialContactInfo(ContactInfoEditPartialRQ objRQ)
        {
            return _contactInfoCommand.EditPartial(objRQ);
        }

        [HttpPost]
        public ApiResult<bool> DeleteContactInfo(IdRQ objRQ)
        {
            return _contactInfoCommand.DeleteByID(new List<long>() { objRQ.ID ?? 0 });
        }
    }
}
