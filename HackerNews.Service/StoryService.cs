using AutoMapper;
using HackerNews.Domain.Config;
using HackerNews.Domain.Repository;
using HackerNews.Service.Dto;
using HackerNews.Service.Interface;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Service
{
    public class StoryService	: IStoryService
	{
        private readonly IMapper _mapper;
        private IStoryRepository _storyRepository;
        private readonly int _pageSize;

		public StoryService(IOptions<HackerNewsConfig> configuration, IMapper mapper, IStoryRepository storyRepository)
		{
            _pageSize = configuration.Value.PageSize;
            _mapper = mapper;
            _storyRepository = storyRepository;
        }

		public async Task<IEnumerable<Story>> GetBestStories()
		{
            var bestStoriesIds = await _storyRepository.GetBestStoriesIds();
            var bestStories = await _storyRepository.GetStoryByIds(bestStoriesIds.Take(_pageSize));

            bestStories = bestStories.OrderByDescending(x => x.score);
            return _mapper.Map<IEnumerable<Story>>(bestStories);
		}
	}
}
