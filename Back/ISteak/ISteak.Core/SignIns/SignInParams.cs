using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Core.SignIns
{
    public class SignInParams
    {
        [JsonProperty("accessKey")]
        public string AccessKey { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
