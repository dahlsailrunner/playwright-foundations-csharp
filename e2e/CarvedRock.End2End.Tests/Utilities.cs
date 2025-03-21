﻿namespace CarvedRock.End2End.Tests;

public static class Utilities
{
    public static string GetBaseUrl()
    {
        //return "https://localhost:7224";
        return TestContext.Parameters.Get("BaseUrl", "https://carvedrock-webapp.whiteglacier-d72dac78.eastus2.azurecontainerapps.io/");
    }
    public static string GetApiUrl()
    {
        //return "https://localhost:7213";
        return TestContext.Parameters.Get("ApiUrl", "https://localhost.nowhere:7213");
    }
}
