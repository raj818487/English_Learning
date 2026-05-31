using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EnglishLearning.ApiTests;

public sealed class DailyVocabularyEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public DailyVocabularyEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GenerateAndGetDailyWords_ReturnsOk()
    {
        var date = "2026-05-31";

        var generateResponse = await _client.PostAsync($"/api/vocabulary/daily/generate?count=5&date={date}", null);
        Assert.Equal(HttpStatusCode.OK, generateResponse.StatusCode);

        var generatedBody = await generateResponse.Content.ReadAsStringAsync();
        Assert.Contains("\"success\":true", generatedBody, StringComparison.OrdinalIgnoreCase);

        var getResponse = await _client.GetAsync($"/api/vocabulary/daily?date={date}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var getBody = await getResponse.Content.ReadAsStringAsync();
        Assert.Contains("\"data\":[", getBody, StringComparison.OrdinalIgnoreCase);
    }
}
