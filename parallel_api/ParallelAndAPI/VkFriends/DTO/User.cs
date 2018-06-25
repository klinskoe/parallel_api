using Newtonsoft.Json;

namespace VkFriends.DTO
{
    class User
    {
        [JsonProperty("uid")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("photo_100")]
        public string PhotoUrl { get; set; }
    }
}
