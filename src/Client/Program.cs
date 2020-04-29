using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static async Task Main()
        {
            var client = new HttpClient();
            var discoveryDocument = await client
                .GetDiscoveryDocumentAsync("http://localhost:5000");
            
            if(discoveryDocument.IsError){
                Console.WriteLine(discoveryDocument.Error);
                return;
            }

            var tokenResponse = await client
                .RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest{
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client",
                    ClientSecret = "secret",
                    Scope = "api1"
                });
            
            if(tokenResponse.IsError){
                Console.WriteLine(tokenResponse.IsError);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:5001/identity");

            if(!response.IsSuccessStatusCode){
                Console.WriteLine(response.StatusCode);
            }
            else{
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
