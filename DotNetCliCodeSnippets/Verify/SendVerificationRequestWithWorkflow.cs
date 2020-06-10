﻿using Nexmo.Api;
using Nexmo.Api.Request;
using Nexmo.Api.Verify;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCliCodeSnippets.Verify
{
    public class SendVerificationRequestWithWorkflow : ICodeSnippet
    {
        public void Execute()
        {
            var NEXMO_API_KEY = Environment.GetEnvironmentVariable("NEXMO_API_KEY") ?? "NEXMO_API_KEY";
            var NEXMO_API_SECRET = Environment.GetEnvironmentVariable("NEXMO_API_SECRET") ?? "NEXMO_API_SECRET";
            var RECIPIENT_NUMBER = Environment.GetEnvironmentVariable("RECIPIENT_NUMBER") ?? "RECIPIENT_NUMBER";
            var BRAND_NAME = Environment.GetEnvironmentVariable("BRAND_NAME") ?? "BRAND_NAME";

            var credentials = Credentials.FromApiKeyAndSecret(NEXMO_API_KEY, NEXMO_API_SECRET);
            var client = new NexmoClient(credentials);

            var request = new VerifyRequest() { Brand = BRAND_NAME, Number = RECIPIENT_NUMBER, WorkflowId = VerifyRequest.Workflow.TTS };
            var response = client.VerifyClient.VerifyRequest(request);

            Console.WriteLine($"Verify Request Complete\nStatus:{response.Status}\nRequest ID:{response.RequestId}");
        }
    }
}
