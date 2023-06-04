using Newtonsoft.Json;
using WMS.Core.Models.DocumentModels.Persons;

namespace WMS.UI.Pages
{
    public partial class Person
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetDate();
        }

        private readonly HttpClient _httpClient = new HttpClient();
        public async Task GetDate()
        {
            try
            {
                var id = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6");
                var apiUrl = "https://localhost:5903";
                _httpClient.BaseAddress = new Uri(apiUrl);
                var response = await _httpClient.GetAsync($"/api/person/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var itemDto = JsonConvert.DeserializeObject<PersonListViewModel>(content);
                    Console.WriteLine(itemDto);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}