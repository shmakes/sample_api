using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SampleApi
{
    public class Flight
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        [DataMember(IsRequired = true)]
        public int HubId { get; set; }

        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime FlightDate { get; set; }
    }
}