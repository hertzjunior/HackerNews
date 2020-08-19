using HackerNews.Domain.Config;
using HackerNews.Domain.Model;
using HackerNews.Domain.Repository;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Data.Repository
{
    public class StoryRepository : IStoryRepository
	{
		private IRestClient _restClient;

		public StoryRepository(IOptions<HackerNewsConfig> configuration)
		{
			_restClient = new RestClient(configuration.Value.UrlBase);
		}

		public async Task<IEnumerable<int>> GetBestStoriesIds()
		{
            var request = new RestRequest("beststories.json", Method.GET);
            var response = await _restClient.ExecuteAsync(request);
            var bestStoriesIds = JsonConvert.DeserializeObject<IEnumerable<int>>(response.Content);

            return bestStoriesIds;
        }

        public async Task<IEnumerable<Story>> GetStoryByIds(IEnumerable<int> ids)
        {            
            var bag = new ConcurrentBag<Story>();
            Parallel.ForEach(ids, currentId =>
            {
                var request = new RestRequest($"item/{currentId}.json ", Method.GET);
                var response = _restClient.ExecuteAsync(request).GetAwaiter().GetResult();
                bag.Add(JsonConvert.DeserializeObject<Story>(response.Content));
            });

            return bag.ToArray();
        }
    }
}
