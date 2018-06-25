using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using VkFriends.DTO;

namespace VkFriends
{
    class VkService
    {
        private const string _apiUrl = "https://api.vk.com/method";

        private string MethodUrl(string methodName) => $"{_apiUrl}/{methodName}";

        private string GetUserUrl(string screenName)
        {
            return $"{MethodUrl("users.get")}?user_ids={screenName}";
        }

        private string GetFriendsUrl(int userId, int count = 20)
        {
            return $"{MethodUrl("friends.get")}?user_id={userId}&fields=nickname,photo_100&count={count}";
        }

        public async Task<List<User>> GetFriends(string screenName, int count = 20)
        {
            using (var client = new HttpClient())
            {
                var user = await GetUser(client, screenName);

                if (user != null)
                {
                    var jsonResponse = await client.GetStringAsync(GetFriendsUrl(user.Id, count));
                    var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
                    return response.Users;
                }
                else
                    return null;
            }
        }

        private async Task<User> GetUser(HttpClient client, string screenName)
        {
            var jsonResponse = await client.GetStringAsync(GetUserUrl(screenName));
            var response = JsonConvert.DeserializeObject<Response>(jsonResponse);

            if (response.Users?.Count >= 1)
                return response.Users[0];
            else
                return null;
        }
    }
}