using HackerNews.Service.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Service.Interface
{
	public interface IStoryService
	{
		Task<IEnumerable<Story>> GetBestStories();
	}
}
