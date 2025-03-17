using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Newtonsoft.Json;

namespace CVtesting;

public class Translate
{

    private static async Task<String> GetKey()
    {
        const string keyName = "translationKey";
        var kvName = "CVbutbetter";
        var kvUri = $"https://{kvName}.vault.azure.net";

        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        var secret = await client.GetSecretAsync(keyName);
        string key = secret.Value.Value;

        return key;
    }

    public static async Task<String> ToCzech()
    {
        string endpoint = "https://api.cognitive.microsofttranslator.com";
        string key = GetKey().Result;
        string route = "/translate?api-version=3.0&from=en&to=cs";
        string location = "germanywestcentral";

        string textEN = string.Join("\n", InputParser.ExtractTextFromDocx(InputParser.testpath)); // <-- Comment out
        //string textEN = <parameter>;
        object[] textObject = new object[] { new { Text = textEN }}; 
        var textJson = JsonConvert.SerializeObject(textObject);

        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage()) {
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(textJson, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);

            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            string result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}




