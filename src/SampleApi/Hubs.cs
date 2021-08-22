using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SampleApi
{
    public class Hub
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }

        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        [DataMember(IsRequired = true), DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Main Contact Email is required")]
        public string MainContactEmail { get; set; }

        [DataMember(IsRequired = false), DataType(DataType.EmailAddress)]
        public string AlternateEmail { get; set; }

        public List<Flight> Flights { get; set; }
    }
}