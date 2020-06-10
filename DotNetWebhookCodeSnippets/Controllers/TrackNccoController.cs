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

namespace DotnetWebhookCodeSnippets.Controllers
{
    [Route("[controller]")]
    public class TrackNccoController : Controller
    {
        [HttpGet("webhooks/answer")]
        public string Answer()
        {
            var sitebase = $"{Request.Scheme}://{Request.Host}";
            sitebase = "http://ngrok.io.slorello.ngrok.io/TrackNcco";

            var talkAction = new TalkAction() { Text = "Thanks for calling the notification line" };
            var notifyAction = new NotifyAction()
            {
                EventUrl = new[] { $"{sitebase}/webhooks/notification" },
                Payload = new FooBar() { Foo = "bar" }
            };
            var talkAction2 = new TalkAction() { Text = "You will never hear me as the notification URL will return an NCCO" };
            var ncco = new Ncco(talkAction, notifyAction, talkAction2);
            return ncco.ToString();
        }

        [HttpPost("webhooks/notification")]
        public string Notify()
        {
            string json;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                json = reader.ReadToEndAsync().Result;                
            }
            var notification = JsonConvert.DeserializeObject<Notification<FooBar>>(json);

            Console.WriteLine($"Notification received payload's foo = {notification.Payload.Foo}");
            var talkAction = new TalkAction() { Text = "Your notification has been received, loud and clear" };
            var ncco = new Ncco(talkAction);
            return ncco.ToString();
        }

        public class FooBar
        {
            public string Foo { get; set; }
        }
    }
}