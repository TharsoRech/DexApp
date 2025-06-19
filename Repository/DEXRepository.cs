using DexApp.Repository.Interfaces;
using System.Net.Http.Headers;
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
            var payload = new
            {
                MachineName = machineName,
                DexFileContent = reportContent
            };

            var jsonString = System.Text.Json.JsonSerializer.Serialize(payload);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Add Basic Auth header
            //Maybe with more time put this info on app settings
            var username = "vendsys";
            var password = "NFsZGmHAGWJSZ#RuvdiV";
            var authHeader = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

            var response = await _httpClient.PostAsync(apiUrl, content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}