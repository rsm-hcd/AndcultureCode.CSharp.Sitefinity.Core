using System;

namespace AndcultureCode.CSharp.Sitefinity.Core.Models.Content.Addresses
{
    public class CoreAddressDto
    {
        public string City { get; set; }
        public string CountryCode { get; set; }
        public Guid Id { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? MapZoomLevel { get; set; }
        public string StateCode { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }

    }
}
