namespace BudgetManager.DataAccess.Models
{
    public class CategoryItem: IIdEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Value { get; set; }
    }
}