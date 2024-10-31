using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BudgetManager.Api.Models.Controllers;

public class Category
{
    [BindRequired]
    public string CategoryName { get; set; }
}