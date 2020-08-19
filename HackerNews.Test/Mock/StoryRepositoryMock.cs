using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Domain.Model;
using HackerNews.Domain.Repository;

namespace HackerNews.Test.Mock
{
    public class StoryRepositoryMock : IStoryRepository
    {
        protected FakeDatabase _db;

        public StoryRepositoryMock()
        {
            _db = new FakeDatabase();
        }

        public Task<IEnumerable<int>> GetBestStoriesIds()
        {
            return Task.FromResult<IEnumerable<int>>(_db.Get<Story>().Select(x => x.id).ToList());
        }

        public Task<IEnumerable<Story>> GetStoryByIds(IEnumerable<int> ids)
        {
            return Task.FromResult<IEnumerable<Story>>(_db.Get<Story>().ToList());
        }
    }
}
