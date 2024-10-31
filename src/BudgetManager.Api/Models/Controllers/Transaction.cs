using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetManager.Api.Models.Controllers;

public class Transaction
{
    [BindRequired]
    public int CategoryId { get; set; }
    [BindRequired]
    public int AccountId { get; set; }

    [BindRequired]
    public DateTime Date { get; set; }
    [BindRequired]
    public string Description { get; set; }
    [BindRequired]
    public decimal Amount { get; set; }
    [BindRequired]
    public string Currency { get; set; }
}