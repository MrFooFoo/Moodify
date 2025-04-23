using System.Net.Http.Headers;
using System.Text.Json;

namespace MoodDetectionService.Services;

public class FaceService
{
    private readonly HttpClient _httpClient;
    private readonly string _subscriptionKey;
    private readonly string _endpoint;

    public FaceService(IConfiguration config, IHttpClientFactory httpClientFactory)
    {
        _subscriptionKey = config["FaceApi:Key"]!;
        _endpoint = config["FaceApi:Endpoint"]!;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<string?> DetectMoodAsync(Stream imageStream)
    {
        var requestUrl = $"{_endpoint}face/v1.0/detect?returnFaceAttributes=emotion";

        using var content = new StreamContent(imageStream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        using var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
        request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
        request.Content = content;

        using var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        using var stream = await response.Content.ReadAsStreamAsync();
        var doc = await JsonDocument.ParseAsync(stream);

        var emotionScores = doc.RootElement.EnumerateArray()
            .FirstOrDefault()
            .GetProperty("faceAttributes")
            .GetProperty("emotion");

        var topEmotion = emotionScores.EnumerateObject()
            .OrderByDescending(e => e.Value.GetDouble())
            .FirstOrDefault();

        return topEmotion.Name;
    }
}
