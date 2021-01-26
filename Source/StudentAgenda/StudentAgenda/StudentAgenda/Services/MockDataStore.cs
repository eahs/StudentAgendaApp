using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAgenda.Models;
using StudentAgenda.ViewModels.AboutUs;

namespace StudentAgenda.Services
{
    public class MockDataStore : IDataStore<Class>
    {
        readonly List<Class> items;

        public MockDataStore()
        {
            items = new List<Class>()
            {
                new Class { Id = Guid.NewGuid().ToString(), Text = "First Class", Description="This is an class description." },
                new Class { Id = Guid.NewGuid().ToString(), Text = "Second Class", Description="This is an class description." },
                new Class { Id = Guid.NewGuid().ToString(), Text = "Third Class", Description="This is an class description." },
                new Class { Id = Guid.NewGuid().ToString(), Text = "Fourth Class", Description="This is an class description." },
                new Class { Id = Guid.NewGuid().ToString(), Text = "Fifth Class", Description="This is an class description." },
                new Class { Id = Guid.NewGuid().ToString(), Text = "Sixth Class", Description="This is an class description." }
            };
        }



        public MockDataStore(List<Class> items)
        {
            this.items = items;
        }

        public async Task<bool> AddItemAsync(Class item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Class item)
        {
            var oldItem = items.Where((Class arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Class arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Class> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Class>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

    }
}