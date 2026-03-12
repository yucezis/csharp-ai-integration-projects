using Newtonsoft.Json;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        string ApiKey = "key";


        Console.Write("enter prompt: ");
        string prompt = Console.ReadLine();

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
            var requestBody = new
            {
                prompt = prompt,
                size = "1024x1024",
                n = 1
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
            string responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

        };
    }

}