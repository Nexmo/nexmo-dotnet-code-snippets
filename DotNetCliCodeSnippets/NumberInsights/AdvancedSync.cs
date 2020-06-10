﻿using Nexmo.Api;
using Nexmo.Api.NumberInsights;
using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCliCodeSnippets.NumberInsights
{
    public class AdvancedSync : ICodeSnippet
    {
        public void Execute()
        {
            var NEXMO_API_KEY = Environment.GetEnvironmentVariable("NEXMO_API_KEY") ?? "NEXMO_API_KEY";
            var NEXMO_API_SECRET = Environment.GetEnvironmentVariable("NEXMO_API_SECRET") ?? "NEXMO_API_SECRET";
            var INSIGHT_NUMBER = Environment.GetEnvironmentVariable("INSIGHT_NUMBER") ?? "INSIGHT_NUMBER";            

            var creds = Credentials.FromApiKeyAndSecret(NEXMO_API_KEY, NEXMO_API_SECRET);
            var client = new NexmoClient(creds);

            var request = new AdvancedNumberInsightRequest() { Number = INSIGHT_NUMBER};
            var response = client.NumberInsightClient.GetNumberInsightAdvanced(request);

            Console.WriteLine($"Advanced insights request status: {response.Status}");
        }
    }
}
