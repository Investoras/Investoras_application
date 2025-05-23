using ClassLibrary.Dto.Transaction;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Clients;


public partial class ListTransaction
{
    [Inject] private HttpClient Http { get; set; } = default!;

    private List<TransactionDto>? transactions;

    //protected override async Task OnInitializedAsync()
    protected async Task OnInitializedAsync()
    {
        transactions = await Http.GetFromJsonAsync<List<TransactionDto>>("https://localhost:7214/Transaction/All");
    }
}
