using System;
using System.Globalization;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Look.Models;
using System.IO;

namespace Look.Services
{
    public class AddressCheck
    {
        public static Root Validate(string zip, string number, string suffix, string street, string city)
        {
            var token = "873c05369f00770deef375bd6e4ba5e3";
            using(WebClient client = new WebClient())
            {
                try
                {
                    var reply = client.DownloadString(string.Format("https://api.spikkl.nl/geo/nld/lookup.json?key={0}&postal_code={1}&street_number={2}&street_number_suffix={3}&street_name={4}&city={5}", token, zip, number, suffix, street, city));
                    
                    Root address = JsonConvert.DeserializeObject<Root>(reply);

                    return address;
                }
                catch(WebException exception)
                {
                    Console.WriteLine(exception.Message);
                    Root address = new Root {status = "failed"};
                    return address;
                }
            }
        }
    }
}