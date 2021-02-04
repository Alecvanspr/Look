using System;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Look.Services
{
    public static class GetTime
    {
        public static string GetTimeSince(this DateTime dateTime)
        {
            string result = string.Empty;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = timeSpan.Seconds > 1 ? 
                    String.Format("ongeveer {0} seconden geleden", timeSpan.Seconds) :
                    "een seconde geleden";
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ? 
                    String.Format("ongeveer {0} minuten geleden", timeSpan.Minutes) :
                    "ongeveer een minuut geleden";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ? 
                    String.Format("ongeveer {0} uur geleden", timeSpan.Hours) : 
                    "ongeveer een uur geleden";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ? 
                    DateTime.Now.ToString("dd MMMM yyyy HH:mm") :
                    DateTime.Now.ToString("dd MMMM yyyy HH:mm");
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ? 
                    DateTime.Now.ToString("dd MMMM yyyy HH:mm") :
                    DateTime.Now.ToString("dd MMMM yyyy HH:mm");
            }
            else
            {
                result = timeSpan.Days > 365 ? 
                    DateTime.Now.ToString("dd MMMM yyyy HH:mm") :
                    DateTime.Now.ToString("dd MMMM yyyy HH:mm");
            }

            return result;
        }
    }
}