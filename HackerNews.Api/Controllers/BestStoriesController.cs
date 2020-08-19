using HackerNews.Service.Dto;
using HackerNews.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

namespace HackerNews.Api.Controllers
{
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        private IMemoryCache _memoryCache;
        private IStoryService _storyService;

        public BestStoriesController(IMemoryCache memoryCache, IStoryService storyService)
        {
            _memoryCache = memoryCache;
            _storyService = storyService;
        }
       
        [HttpGet, Route("api/beststories")]
        [SwaggerOperation(Summary = "List of best stories", OperationId = "List Best Stories", Description = "List of best stories ")]
        [SwaggerResponse(200, "List OK", typeof(IEnumerable<Story>))]
        [SwaggerResponse(400, "Bad Request")]
        public IActionResult Get()
        {
            //cria um cache local de 30 segundos 
            var bestStories = _memoryCache.GetOrCreate<IEnumerable<Story>>(
            "BestStories", context =>
            {
                context.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                context.SetPriority(CacheItemPriority.High);

                return _storyService.GetBestStories().GetAwaiter().GetResult();
            });

            return Ok(bestStories);
        }
    }
}