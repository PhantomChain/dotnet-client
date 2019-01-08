using System;
using System.Collections.Generic;
using PhantomChain.Client.API.Models;
using Newtonsoft.Json;

namespace PhantomChain.Client.API
{
    [JsonObject]
    public class Response<T>
    {
        public Meta Meta { get; set; }
        public T Data { get; set; }
    }
}
