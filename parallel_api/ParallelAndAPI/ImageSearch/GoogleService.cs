using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GoogleSearch.DTO;

namespace GoogleSearch
{
	class GoogleService
	{
		private string _engineId = "";
		private string _appKey = "";

		const string ImageDir = "images";
		const int BufferSize = 4096;

        private string GetUrl(string query)
        {
            return $"https://www.googleapis.com/customsearch/v1?q={query}&cx={_engineId}&key={_appKey}&searchType=image";
        }

		public async Task<List<SearchResult>> GetResults(string query)
		{
			using (var client = new HttpClient())
			{
				var result = await client.GetStringAsync(GetUrl(query));
				var data = JsonConvert.DeserializeObject<Result>(result);

                // Convertion from DTO to Domain Model
                var images = await Task.WhenAll(data.Items.Select(async item => new SearchResult
                {
                    Title = item.Title,
                    Description = item.Snippet,
                    Url = item.Link,
                    Path = await DownloadFile(client, item.Image.ThumbnailLink)
                }));

                return images.ToList();
			}
		}

        private async Task<string> DownloadFile(HttpClient client, string url)
        {
			// Create the target directory if it does not exist
			if (!Directory.Exists(ImageDir))
				Directory.CreateDirectory(ImageDir);
			var resultingFileName = Path.Combine(Environment.CurrentDirectory, ImageDir, Path.GetFileName(url));

			// File already exists, skip the download process
			if (File.Exists(resultingFileName))
				return resultingFileName;

			using (var fileStream = new FileStream(resultingFileName, FileMode.Create))
			{
				var imageStream = await client.GetStreamAsync(url);
				byte[] buffer = new byte[BufferSize];
				int readBytes;
				do
				{
					readBytes = await imageStream.ReadAsync(buffer, 0, BufferSize);
					if (readBytes != 0)
						fileStream.Write(buffer, 0, readBytes);
				} while (readBytes != 0);

				return resultingFileName;
			}
		}
	}
}