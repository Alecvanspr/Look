using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Look.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class AdministrativeArea    {
        public string type { get; set; } 
        public string name { get; set; } 
        public string abbreviation { get; set; } 
    }

    public class Country    {
        public string iso3_code { get; set; } 
        public string iso2_code { get; set; } 
        public string name { get; set; } 
    }

    public class Centroid    {
        public double latitude { get; set; } 
        public double longitude { get; set; } 
    }

    public class Result    {
        public string location_id { get; set; } 
        public string postal_code { get; set; } 
        public int street_number { get; set; } 
        public object street_number_suffix { get; set; } 
        public string street_name { get; set; } 
        public string city { get; set; } 
        public string municipality { get; set; } 
        public List<AdministrativeArea> administrative_areas { get; set; } 
        public Country country { get; set; } 
        public Centroid centroid { get; set; } 
        public string formatted_address { get; set; } 
        public string match { get; set; } 
    }

    public class Meta    {
        public long timestamp { get; set; } 
        public string trace_id { get; set; } 
    }

    public class Suggestions    {
        public string street_number_suffix { get; set; } 
    }

    public class Root    {
        public List<Result> results { get; set; } 
        public string status { get; set; } 
        public Meta meta { get; set; } 
        public string status_code { get; set; } 
        public Suggestions suggestions { get; set; } 
    }


}