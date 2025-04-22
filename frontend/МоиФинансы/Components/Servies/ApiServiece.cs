namespace МоиФинансы.Components.Servies
{
    using МоиФинансы.Components.Shared.Models;
    using System.Net.Http.Json;
    

    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://localhost:5001/api/");
        }

        // GET: transactions
        public async Task<List<TransactionDto>> GetTransactionsAsync()
        {
            return await _http.GetFromJsonAsync<List<TransactionDto>>("transactions");
        }

        // POST: transactions
        public async Task<HttpResponseMessage> AddTransactionAsync(TransactionDto dto)
        {
            return await _http.PostAsJsonAsync("transactions", dto);
        }

        // Other methods for handling transactions...
    }
}
