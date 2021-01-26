using StudentAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentAgenda.Services
{
    public class AssignmentDataStore : IDataStore<Assignment>
    {
        readonly List<Assignment> items;

        public AssignmentDataStore()
        {
            items = new List<Assignment>()
            {
                new Assignment { Id = Guid.NewGuid().ToString(), Text = "First Class", Description="This is an class description." },
                new Assignment { Id = Guid.NewGuid().ToString(), Text = "Second Class", Description="This is an class description." },
                new Assignment { Id = Guid.NewGuid().ToString(), Text = "Third Class", Description="This is an class description." },
                new Assignment { Id = Guid.NewGuid().ToString(), Text = "Fourth Class", Description="This is an class description." },
                new Assignment { Id = Guid.NewGuid().ToString(), Text = "Fifth Class", Description="This is an class description." },
                new Assignment { Id = Guid.NewGuid().ToString(), Text = "Sixth Class", Description="This is an class description." }
            };
        }



        public AssignmentDataStore(List<Assignment> items)
        {
            this.items = items;
        }

        public async Task<bool> AddItemAsync(Assignment item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Assignment item)
        {
            var oldItem = items.Where((Assignment arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Assignment arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Assignment> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Assignment>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
