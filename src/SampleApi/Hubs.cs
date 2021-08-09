using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SampleApi
{
    public class Hub
    {
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public Guid Id { get; set; }

        [DataMember(IsRequired = true), DataType(DataType.EmailAddress)]
        public string MainContactEmail { get; set; }
    }
}