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

        [HttpGet, Route("{id}")]
        public ApiResult<ContactInfo> Get(long id)
        {
            return _contactInfoCommand.QueryByID(id);
        }

        [HttpGet]
        public ApiResult<PageData<IEnumerable<ContactInfo>>> Get([FromBody] ContactInfoQueryRQ objRQ)
        {
            return _contactInfoCommand.QueryByCondition(objRQ);
        }

        [HttpPost]
        public ApiResult<ContactInfo> Post([FromBody] ContactInfoAddRQ objRQ)
        {
            return _contactInfoCommand.Add(objRQ);
        }

        [HttpPut, Route("{id}")]
        public ApiResult<ContactInfo> Put(long id, [FromBody] ContactInfoRestfulEditRQ objRQ)
        {
            return _contactInfoCommand.Edit(new ContactInfoEditRQ()
            {
                ID = id,
                Name = objRQ.Name,
                Nickname = objRQ.Nickname,
                Gender = objRQ.Gender,
                Age = objRQ.Age,
                PhoneNo = objRQ.PhoneNo,
                Address = objRQ.Address
            });
        }

        [HttpPatch, Route("{id}")]
        public ApiResult<ContactInfo> Patch(long id, [FromBody] ContactInfoRestfulEditPartialRQ objRQ)
        {
            return _contactInfoCommand.EditPartial(new ContactInfoEditPartialRQ()
            {
                ID = id,
                Name = objRQ.Name,
                Nickname = objRQ.Nickname,
                Gender = objRQ.Gender,
                Age = objRQ.Age,
                PhoneNo = objRQ.PhoneNo,
                Address = objRQ.Address
            });
        }

        [HttpDelete, Route("{id}")]
        public ApiResult<bool> Delete(long id)
        {
            return _contactInfoCommand.DeleteByID(new List<long>() { id });
        }
    }
}
