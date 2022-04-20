using System.Text.Json;

namespace AristosTest.Web.Utility;

public static class GlobalValues
{
    public const string ApiUrl = "https://localhost:7238/api/airport";

    public static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
}