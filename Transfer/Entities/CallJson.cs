using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json;

namespace Entities
{
    public class CallJson
    {
        public static Call[] GetCalls()
        {
            Call callEmtp = new Call()
            {
                Id = 1,
                AgentId = 4,
                CallExternalId = "5",
                CampaignId = 6,
                DialedPhoneNumber = "45454",
                EndTime = DateTime.UtcNow,
                PhoneNumber = "23123",
                StartTime = DateTime.UtcNow,
                Status = 11,
                WasSentToHub = false
            };
            Random rnd = new Random(DateTime.Now.Millisecond);
            Call[] calls = new Call[rnd.Next(500, 15000)];
            for (int i = 0; i < calls.Length; i++)
            {
                callEmtp.Id = i;
                calls[i] = callEmtp;
            }
            return calls;
        }

        public static string CallsToJson(Call[] arr)
        {
            return JsonConvert.SerializeObject(arr);
        }

        public static Call[] JsonToCalls(string str)
        {
            return JsonConvert.DeserializeObject<Call[]>(str);
        }
    }
}
