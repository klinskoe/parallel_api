using System.Collections.Generic;
using Newtonsoft.Json;

namespace VkFriends.DTO
{
    class Response
    {
        [JsonProperty("response")]
        public List<User> Users { get; set; }
    }
}
