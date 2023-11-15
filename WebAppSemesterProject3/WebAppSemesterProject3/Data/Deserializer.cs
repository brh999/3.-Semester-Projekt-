using Newtonsoft.Json;
using System.Net.Http;
using WebAppSemesterProject3.Data;
using WebAppSemesterProject3.Models;

namespace WebAppSemesterProject3.DTO
{
    public class Deserializer<T> : IDeserializer<T>, IDisposable
    {
        private string apiUrl = "http://localhost:5042/api/currency";

        public async Task<T> GetObject(int id)
        {
            using (HttpClient client = new HttpClient())
            {


                // Modify the url with the ID of the account
                string url = apiUrl + id;
                // Make a GET request and process the response

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Deserialize the response into an object
                string jsonResponse = await response.Content.ReadAsStringAsync();
                T? res = JsonConvert.DeserializeObject<T>(jsonResponse);
                return res;
            }


        }



        public async Task<IEnumerable<T>> GetList()
        {
            using (HttpClient client = new HttpClient())
            {
                // Make a GET request and process the response
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // Deserialize the response into objects
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<T>? res = JsonConvert.DeserializeObject<List<T>>(jsonResponse);
                return res;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
