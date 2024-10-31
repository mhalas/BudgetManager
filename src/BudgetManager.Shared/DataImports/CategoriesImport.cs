using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BudgetManager.DataAccess;
using BudgetManager.DataAccess.Models;

namespace BudgetManager.Shared.DataImports;

public class CategoriesImport(BudgetManagerContext context)
{
    public async Task ImportDataAsync(Dictionary<string, List<string>> categoriesToImport, CancellationToken cancellationToken)
    {
        var existingCategories = context.Category.ToList();
        
        foreach (var category in categoriesToImport)
        {
            var categoryForItems = await GetOrCreateCategory(existingCategories, category.Key, cancellationToken);

            foreach (var item in category.Value)
            {
                await AddItemToDatabaseIfNotExist(categoryForItems, item, cancellationToken);
            }
        }
    }

    private async Task<Category> GetOrCreateCategory(IEnumerable<Category> existingCategories, 
        string categoryName, 
        CancellationToken cancellationToken)
    {
        var existingCategory = existingCategories.SingleOrDefault(x => x.Name == categoryName);

        if (existingCategory != null)
        {
            return existingCategory;
        }
        
        var newCategory = new Category { Name = categoryName };
        var result = await context.Category.AddAsync(newCategory, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        newCategory.Id = result.Entity.Id;
        
            
        return newCategory;
    }

    private async Task AddItemToDatabaseIfNotExist(Category existingCategory, string categoryItemName, CancellationToken cancellationToken)
    {
        if (!existingCategory.CategoryItems.Any(x=> x.Value == categoryItemName))
        {
            await context.CategoryItem.AddAsync(new CategoryItem
            {
                CategoryId = existingCategory.Id,
                Value = categoryItemName
            }, cancellationToken);
            
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}