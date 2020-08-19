using HackerNews.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Test.Mock
{
    public class FakeDatabase
    {
        private readonly List<Story> _stories;
        private readonly List<object> _tables = new List<object>();

        public FakeDatabase()
        {
            _stories = new List<Story>();
            for (int i = 0; i < new Random().Next(20, 40); i++)
            {
                _stories.Add(new Story {
                    url = $"https://hacker-news.firebaseio.com/v0/item/{i+1}.json",
                    by = $"test_{i + 1}",
                    time = 1570887781,
                    descendants = new Random().Next(500, 1000),
                    score = new Random().Next(500, 1000),
                    title = $"test_{i + 1}"
                });
            }

            _tables.Add(_stories);
        }

        public IQueryable<T> Get<T>()
        {
            foreach (var table in _tables)
            {
                if (table is List<T>)
                {
                    List<T> lst = (List<T>)table;

                    return lst.AsQueryable();
                }
            }
            return null;
        }
    }
}
