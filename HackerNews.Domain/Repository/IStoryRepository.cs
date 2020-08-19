using HackerNews.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Domain.Repository
{
	public interface IStoryRepository
	{
        Task<IEnumerable<int>> GetBestStoriesIds();
        Task<IEnumerable<Story>> GetStoryByIds(IEnumerable<int> ids);

    }
}
