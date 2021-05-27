using BlazorDemo.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorDemo.Client.Stores
{
    public class TodoStore
    {

        HttpClient Client { get; set; }
        public List<TodoTableVM> TableVMs { get; set; } = new List<TodoTableVM>();
        public bool LoadingTableData = true;
        public int UnfinishedTasks
        {
            get
            {
                return TableVMs.Where(item => item.Finished == false).Count();
            }
        }

        public TodoStore(HttpClient client)
        {
            Client = client;

        }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public async Task FetchTableVMs()
        {
            LoadingTableData = true;
            TableVMs = await Client.GetFromJsonAsync<List<TodoTableVM>>("http://localhost:5000/todos", cancellationToken: default) ?? new List<TodoTableVM>();
            LoadingTableData = false;
            NotifyStateChanged();
        }

        public async void Delete(int id)
        {
            await Client.DeleteAsync($"http://localhost:5000/todos/{id}", cancellationToken: default);
            TableVMs.RemoveAll(item => item.Id == id);
            NotifyStateChanged();
        }
    }
}
