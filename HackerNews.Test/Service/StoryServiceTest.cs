using AutoMapper;
using HackerNews.Domain.Config;
using HackerNews.Service;
using HackerNews.Service.Interface;
using HackerNews.Test.Mock;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.Test.Service
{
    public class StoryServiceTest
    {
        public IStoryService _storyService { get; set; }
        private readonly IMapper _mapper;

        public StoryServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(HackerNews.Service.Mappings.MappingProfile));
            });
            _mapper = config.CreateMapper();

            var _storyRepository = new StoryRepositoryMock();

            var configuration = new OptionsWrapper<HackerNewsConfig>(new HackerNewsConfig
            {
                UrlBase = "teste",
                PageSize = 20
            });

            _storyService = new StoryService(configuration, _mapper, _storyRepository);
        }

        [Fact]
        public async Task GetBestStories_NotEmpty()
        {
            var models = await _storyService.GetBestStories();

            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task GetBestStories_Success()
        {
            var models = await _storyService.GetBestStories();

            Assert.True(models.Any());
        }
    }
}
