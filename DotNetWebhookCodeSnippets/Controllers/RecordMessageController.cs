﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nexmo.Api.Voice.EventWebhooks;
using Nexmo.Api.Voice.Nccos;

namespace DotNetWebhookCodeSnippets.Controllers
{
    [Route("[controller]")]
    public class RecordMessageController : Controller
    {
        [HttpGet("webhooks/answer")]
        public string Answer()
        {
            var sitebase = $"{Request.Scheme}://{Request.Host}";
            var outGoingAction = new TalkAction() { Text = "Please leave a message after the tone, then press #. We will get back to you as soon as we can" };            
            var recordAction = new RecordAction()
            {
                EventUrl = new string[] { $"{sitebase}/recordmessage/webhooks/recording" },
                EventMethod = "POST",
                EndOnSilence = "3",
                EndOnKey = "#",
                BeepStart="true"
            };
            var thankYouAction = new TalkAction { Text = "Thank you for your message. Goodbye" };
            var ncco = new Ncco(outGoingAction, recordAction, thankYouAction);
            return ncco.ToString();
        }

        [HttpPost("webhooks/recording")]
        public IActionResult Recording()
        {
            Record record;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                record = JsonConvert.DeserializeObject<Record>(reader.ReadToEndAsync().Result);
            }

            Console.WriteLine($"Record event received on webhook - URL: {record?.RecordingUrl}");
            return StatusCode(204);
        }
    }
}