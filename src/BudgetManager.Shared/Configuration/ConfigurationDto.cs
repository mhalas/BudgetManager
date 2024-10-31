using System.Collections.Generic;

namespace BudgetManager.Shared.Configuration
{
    public class ConfigurationDto
    {
        public ConfigurationDto(string defaultCategoryName,
            IDictionary<string, ConfigurationColumnDto> columnDefinitions,
            IDictionary<string, List<string>> categoryDictionary)
        {
            CategoryDictionary = categoryDictionary;
            DefaultCategoryName = defaultCategoryName;
            ColumnDefinitions = columnDefinitions;
        }
        public string DefaultCategoryName { get; set; }
        public IDictionary<string, ConfigurationColumnDto> ColumnDefinitions { get; }
        public IDictionary<string, List<string>> CategoryDictionary { get; }
    }
}
