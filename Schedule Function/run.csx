#r "Newtonsoft.Json"

using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;

public static async Task Run(TimerInfo myTimer, ILogger log)
{
    var donorName = "string"; 
    var units = 0; 

    var requestBody = new
    {
        donorName,
        units
    };

    var apiUrl = "https://19070006002finalbloodsearch.azurewebsites.net/Blood/NightlySearchForBloodRequests";

    using (var httpClient = new HttpClient())
    {
        var jsonContent = JsonConvert.SerializeObject(requestBody);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(apiUrl, httpContent);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            log.LogInformation($"Function executed successfully. Response: {responseBody}");
        }
        else
        {
            log.LogError($"Function failed. StatusCode: {response.StatusCode}");
        }
    }
}
