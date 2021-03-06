﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nexmo.Api.Utility;
using Nexmo.Api.Voice.EventWebhooks;
using Nexmo.Api.Voice.Nccos;
using Nexmo.Api.Voice.Nccos.Endpoints;

namespace DotNetWebhookCodeSnippets.Controllers
{
    [Route("[controller]")]
    public class SplitAudioController : Controller
    {
        [HttpGet("webhooks/answer")]
        public IActionResult Answer()
        {
            var TO_NUMBER = Environment.GetEnvironmentVariable("TO_NUMBER") ?? "TO_NUMBER";
            var NEXMO_NUMBER = Environment.GetEnvironmentVariable("NEXMO_NUMBER") ?? "NEXMO_NUMBER";
            var host = Request.Host.ToString();
            //Uncomment the next line if using ngrok with --host-header option
            //host = Request.Headers["X-Original-Host"];

            var eventUrl = $"{Request.Scheme}://{host}/SplitAudio/webhooks/recording";
            var talkAction = new TalkAction{ Text = "recording call", BargeIn="false" };
            var recordAction = new RecordAction()
            {
                EventUrl = new string[] { eventUrl },
                EventMethod = "POST",
                Channels = 2,
                Split = "conversation"
            };

            var connectAction = new ConnectAction() 
            { 
                From = NEXMO_NUMBER, 
                Endpoint = new[] { new PhoneEndpoint { Number = TO_NUMBER } }, 

            };

            var ncco = new Ncco(talkAction, recordAction, connectAction);            
            return Ok(ncco.ToString());
        }

        [HttpPost("webhooks/recording")]
        public async Task<IActionResult> Recording()
        {
            var record = await WebhookParser.ParseWebhookAsync<Record>(Request.Body, Request.ContentType);
            Console.WriteLine($"Record event received on webhook - URL: {record?.RecordingUrl}");
            return StatusCode(204);
        }

    }
}