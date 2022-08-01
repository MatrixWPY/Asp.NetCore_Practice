using System;

namespace WebApi.Models.Data
{
    public class ContactInfo
    {
        public long ContactInfoID { get; set; }

        public string Name { get; set; }

        public string Nickname { get; set; }

        public EnumGender? Gender { get; set; }

        public int? Age { get; set; }

        public string PhoneNo { get; set; }

        public string Address { get; set; }

        public bool IsEnable { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public enum EnumGender
        {
            Female = 0,
            Male = 1
        }
    }
}
