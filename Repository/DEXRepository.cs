using DexApp.Repository.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace DexApp.Repository;

public class DEXRepository:IDEXRepository
{
    private readonly HttpClient _httpClient = new();
    private readonly string apiUrl = "http://localhost:5005/vdi-dex";

    public async Task<bool> SendDEXFile(string reportContent, string machineName)
    {
        try
        {
            // Create the payload object
            var payload = new
            {
                MachineName = machineName,
                DexFileContent = reportContent
            };

            // Serialize payload to JSON
            var jsonString = JsonConvert.SerializeObject(payload);

            // Set up RestSharp client
            var client = new RestClient(apiUrl);

            // Prepare the request
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");

            // Add Basic Auth header
            var username = "vendsys";
            var password = "NFsZGmHAGWJSZ#RuvdiV";
            var authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            request.AddHeader("Authorization", $"Basic {authHeader}");

            // Add JSON body
            request.AddJsonBody(jsonString);

            // Execute request
            var response = await client.ExecuteAsync(request);

            // Check response
            return response.IsSuccessful;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}