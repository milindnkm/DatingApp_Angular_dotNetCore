using Microsoft.AspNetCore.Http;
using System;

namespace DatingApp.API.Helpers
{
    public static class Extenstion
    {
        /// <summary>
        /// Extenstion method to add the Error header and error message in HttpResponse
        /// </summary>
        /// <param name="response">HttpResponse object</param>
        /// <param name="message">Error message</param>
        public static void AddApplicationError(this HttpResponse response, string message){
            response.Headers.Add("Application-Error",message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
        /// <summary>
        /// Extenstion method to calculate Age
        /// </summary>
        /// <param name="theDateTime">Pass birth date</param>
        /// <returns>Returns Age</returns>
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
                age--;
            return age;
        }
    }
}