using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiUrl = "http://127.0.0.1:8000/predict"; // Replace with the actual FastAPI URL
        string imagePath = "C:\\Users\\Lenovo\\OneDrive\\Desktop\\FinalPro\\FinalPro\\Screenshot 2024-11-02 211226.png";       // Replace with the path to your image

        using (var httpClient = new HttpClient())
        {
            using (var content = new MultipartFormDataContent())
            {
                // Load the image file
                byte[] imageData = File.ReadAllBytes(imagePath);
                var byteArrayContent = new ByteArrayContent(imageData);

                // Set the content type to match the UploadFile parameter in FastAPI
                byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                // Add the image content to the request
                content.Add(byteArrayContent, "file", Path.GetFileName(imagePath));

                // Send the POST request to the FastAPI endpoint
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Get the response content
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Predictions:");
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
