using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class HelloEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HelloEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Hello_ReturnsMessageAndNow()
    {
        var client = _factory.CreateClient();
        var resp = await client.GetAsync("/hello");
        resp.EnsureSuccessStatusCode();
        var json = await resp.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        Assert.True(doc.RootElement.TryGetProperty("message", out var m));
        Assert.Equal("Hello DevOps", m.GetString());

        Assert.True(doc.RootElement.TryGetProperty("now", out var n));
        Assert.False(string.IsNullOrWhiteSpace(n.GetString()));
    }
}
