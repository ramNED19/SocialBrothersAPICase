using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialBrothersAPICase.Model
{
    public class APIClient
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apikey = "2a193780-e490-11eb-a909-43fe68b215f9";
        private static Coordinate GetLocation(Address address)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            string queryString = "" + address.Huisnummer + "%20" + address.Straat + "%2C%20" + address.Plaats + "%2C%20" + address.Land + "";
            Regex regex = new Regex(@" ");
            string textString = regex.Replace(queryString, "%20");
            string requestString = "https://app.geocodeapi.io/api/v1/search?apikey=" + apikey + "&text=" + textString + "";
            var resultString = client.GetStringAsync(requestString).Result;
            var result = JObject.Parse(resultString);
            var coordinateList = result["features"][0]["geometry"]["coordinates"].ToObject<IList<double>>();
            return new Coordinate() { Latitude = coordinateList[1], Longtitude = coordinateList[0] };
        }

        public static double GetDistance(Address address1, Address address2)
        {
            Coordinate coordinate1 = GetLocation(address1);
            Coordinate coordinate2 = GetLocation(address2);

            //Haversine formula for distance
            const double r = 6372.8;
            const double toRadian = Math.PI / 180;
            var dlat = toRadian * (coordinate2.Latitude - coordinate1.Latitude);
            var dlon = toRadian * (coordinate2.Longtitude - coordinate1.Longtitude);

            var q = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) +
                Math.Cos(toRadian * (coordinate1.Latitude)) * Math.Cos(toRadian * (coordinate2.Latitude)) *
                Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(q)));

            var d = r * c;
            return d;
        }
    }
}
