using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SampleApi.Models
{
    public class Flight
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        [DataMember(IsRequired = true)]
        public int HubId { get; set; }

        [DataMember(IsRequired = true)]
        [StringLength(15)]
        public string Name { get; set; }

        [DataMember(IsRequired = true)]
        public DateTime FlightDate { get; set; }
    }
}