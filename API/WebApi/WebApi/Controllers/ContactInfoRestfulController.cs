using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Commands.Interface;
using WebApi.Filters;
using WebApi.Models.Data;
using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ValidRequest]
    [ApiController]
    public class ContactInfoRestfulController : ControllerBase
    {
        private readonly IContactInfoCommand _contactInfoCommand;

        public ContactInfoRestfulController(IContactInfoCommand contactInfoCommand)
        {
            _contactInfoCommand = contactInfoCommand;
        }

        [HttpGet]
        public Result<ContactInfo> Get(IdRQ objRQ)
        {
            return _contactInfoCommand.QueryByID(objRQ.ID ?? 0);
        }

        [HttpPost]
        public Result<ContactInfo> Post(ContactInfoAddRQ objRQ)
        {
            return _contactInfoCommand.Add(objRQ);
        }

        [HttpPut]
        public Result<ContactInfo> Put(ContactInfoEditRQ objRQ)
        {
            return _contactInfoCommand.Edit(objRQ);
        }

        [HttpPatch]
        public Result<ContactInfo> Patch(ContactInfoEditPartialRQ objRQ)
        {
            return _contactInfoCommand.EditPartial(objRQ);
        }

        [HttpDelete]
        public Result<bool> Delete(IdRQ objRQ)
        {
            return _contactInfoCommand.DeleteByID(new List<long>() { objRQ.ID ?? 0 });
        }
    }
}
