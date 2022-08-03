using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Commands.Interface;
using WebApi.Filters;
using WebApi.Models.Data;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Controllers
{
    [Route("V1")]
    [ValidRequest]
    [ApiController]
    public class ContactInfoARController : ControllerBase
    {
        private readonly IContactInfoCommand _contactInfoCommand;

        public ContactInfoARController(IContactInfoCommand contactInfoCommand)
        {
            _contactInfoCommand = contactInfoCommand;
        }

        [HttpPost, Route("C101")]
        public ApiResult<ContactInfo> GetContactInfo(IdRQ objRQ)
        {
            return _contactInfoCommand.QueryByID(objRQ.ID ?? 0);
        }

        [HttpPost, Route("C102")]
        public ApiResult<PageData<IEnumerable<ContactInfo>>> ListContactInfo(ContactInfoQueryRQ objRQ)
        {
            return _contactInfoCommand.QueryByCondition(objRQ);
        }

        [HttpPost, Route("C103")]
        public ApiResult<ContactInfo> AddContactInfo(ContactInfoAddRQ objRQ)
        {
            return _contactInfoCommand.Add(objRQ);
        }

        [HttpPost, Route("C104")]
        public ApiResult<ContactInfo> EditContactInfo(ContactInfoEditRQ objRQ)
        {
            return _contactInfoCommand.Edit(objRQ);
        }

        [HttpPost, Route("C105")]
        public ApiResult<ContactInfo> EditPartialContactInfo(ContactInfoEditPartialRQ objRQ)
        {
            return _contactInfoCommand.EditPartial(objRQ);
        }

        [HttpPost, Route("C106")]
        public ApiResult<bool> DeleteContactInfo(IdRQ objRQ)
        {
            return _contactInfoCommand.DeleteByID(new List<long>() { objRQ.ID ?? 0 });
        }
    }
}
