﻿using System.Collections.Generic;
using WebApi.Commands.Interface;
using WebApi.Models.Data;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Services.Interface;

namespace WebApi.Commands.Instance
{
    public class ContactInfoCommand : BaseCommand, IContactInfoCommand
    {
        private readonly IContactInfoService _contactInfoService;

        public ContactInfoCommand(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        public Result<ContactInfo> QueryByID(long id)
        {
            var res = _contactInfoService.Query(id);
            return res == null ? FailRP<ContactInfo>(1, "No Data") : SuccessRP(res);
        }

        public Result<bool> Add(ContactInfoAddRQ objRQ)
        {
            var res = _contactInfoService.Insert(new ContactInfo()
            {
                Name = objRQ.Name,
                Nickname = objRQ.Nickname,
                Gender = (ContactInfo.EnumGender?)objRQ.Gender,
                Age = objRQ.Age,
                PhoneNo = objRQ.PhoneNo,
                Address = objRQ.Address
            });
            return res == false ? FailRP<bool>(2, "Add Fail") : SuccessRP(res);
        }

        public Result<bool> Edit(ContactInfoEditRQ objRQ)
        {
            var res = _contactInfoService.Update(new ContactInfo()
            {
                ContactInfoID = objRQ.ID ?? 0,
                Name = objRQ.Name,
                Nickname = objRQ.Nickname,
                Gender = (ContactInfo.EnumGender?)objRQ.Gender,
                Age = objRQ.Age,
                PhoneNo = objRQ.PhoneNo,
                Address = objRQ.Address
            });
            return res == false ? FailRP<bool>(3, "Edit Fail") : SuccessRP(res);
        }

        public Result<bool> DeleteByID(IEnumerable<long> liID)
        {
            var res = _contactInfoService.Delete(liID);
            return res == false ? FailRP<bool>(4, "Delete Fail") : SuccessRP(res);
        }
    }
}