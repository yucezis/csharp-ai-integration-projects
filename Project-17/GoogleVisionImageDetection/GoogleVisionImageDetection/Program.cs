using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoogleVisionImageDetection
{
    class Program
    {
        private static readonly string GoogleApiKey= "API KEY";
        private static readonly string imagePath = "C:/Users/Public/indir.jpg";

        static async Task Main()
        {
            Console.WriteLine("IMAGE DETECTION WITH GOOGLE VISION");
            string response = await DetectObjects(imagePath);

            Console.WriteLine("******************************");
            Console.WriteLine(response);

            Console.ReadLine();
        }

        static async Task<string> DetectObjects(string path) 
        {
            using var client = new HttpClient();

            string apiUrl = $"https://vision.googleapis.com/v1/images:annotate?key={GoogleApiKey}";
            byte[] imageBytes = File.ReadAllBytes(path);
            string base64Image = Convert.ToBase64String(imageBytes);

            var requestBody = new
            {
                requests = new[]
                {
                    new{
                    image = new { content = base64Image },
                    features = new[] { new { type = "LABEL_DETECTION", maxResults = 10 } }
                    }
                }
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage response=await client.PostAsync(apiUrl, jsonContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
