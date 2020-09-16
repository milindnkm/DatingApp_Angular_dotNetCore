using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        /// <summary>
        /// Extention method to add Pagination details to Response header.
        /// </summary>
        /// <param name="response">HttpReponse object</param>
        /// <param name="currentPage">Current Page number</param>
        /// <param name="itemsPerPage">Records per page</param>
        /// <param name="totalItems">Total Records</param>
        /// <param name="totalPages">Total pages</param>
        public static void AddPagination(this HttpResponse response, 
            int currentPage, int itemsPerPage, int totalItems, int totalPages )
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader,camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }
    }
}