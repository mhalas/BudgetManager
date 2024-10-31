namespace BudgetManager.DataAccess.Models
{
    public class Category: IIdEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<CategoryItem> CategoryItems { get; set; } = new List<CategoryItem>();
    }
}
