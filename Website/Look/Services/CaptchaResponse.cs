using System;
using System.Globalization;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Look.Services
{
    public class CaptchaResponse
    {
        public static string Validate(string EncodedResponse)
        {
            var client = new WebClient();
            string secret = "6Lf50P8ZAAAAAPBibNSCSw-H8YzyTrWzmu_GGrtp";
            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, EncodedResponse));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(GoogleReply);

            return captchaResponse.Success.ToLower();
        }

        [JsonProperty("success")]
        public string Success { get; set; }
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}