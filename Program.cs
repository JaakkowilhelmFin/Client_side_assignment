

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
/*Make sure that NuGet additions/packages are installed */
namespace MusicGenreGenerator
{
    // Interface definition
    public interface IApiService
    {
        Task<List<string>> GetStringsAsync();
    }

    //  and its implementaion ( interface API)
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /* Actual implementation of datasource can be seen below*/
        public async Task<List<string>> GetStringsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://binaryjazz.us/wp-json/genrenator/v1/genre/5");
            return JsonConvert.DeserializeObject<List<string>>(response);
        }
    }

    // Main program 
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient(); 
            try
            {
                IApiService apiService = new ApiService(httpClient);
                var result = await apiService.GetStringsAsync();

                Console.WriteLine("Random Music Genres:");
                foreach (var str in result)
                {
                    Console.WriteLine(str);
                }
            }
            finally
            {
                httpClient.Dispose(); 
            }
            Console.WriteLine();
            Console.WriteLine("Press keyboard to exit...");
            Console.ReadLine();
        }
    }
}
