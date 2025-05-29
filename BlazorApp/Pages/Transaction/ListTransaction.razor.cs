using ClassLibrary.Dto.Transaction;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using BlazorApp.Models.Transaction;

namespace BlazorApp.Pages.Transaction;


public partial class ListTransaction
{
    [Inject] private HttpClient Http { get; set; } = default!;

    private List<TransactionDto>? transactions;

    protected override async Task OnInitializedAsync()
    {
        transactions = await Http.GetFromJsonAsync<List<TransactionDto>>("Transaction/All");
    }
}
