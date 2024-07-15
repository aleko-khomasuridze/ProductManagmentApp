using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Globalization;

namespace ProductManagmentApp.Models
{
    public class Filters
    {
        public Filters(string filterString)
        {
            FilterString = filterString ?? "all-all";
            string[] filters = FilterString.Split('-');
            CategoryId = filters[0];
            ExpirationDate = filters[1];
        }

        public string FilterString { get; }
        public string CategoryId { get; }
        public string ExpirationDate { get; }

        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasExpirationDate => ExpirationDate.ToLower() != "all";



        public static Dictionary<string, string> ExpirationDateValues =
            new Dictionary<string, string>
            {
                { "expired", "Expired" },
                { "not-expired", "Not Expired" }
            };


        public bool IsExpired => ExpirationDate.ToLower() == "expired";
        public bool IsNotExpired => ExpirationDate.ToLower() == "not-expired";
    }
}
