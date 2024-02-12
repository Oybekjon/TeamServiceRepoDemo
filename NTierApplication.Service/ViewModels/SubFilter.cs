using System.ComponentModel.DataAnnotations;

namespace NTierApplication.Service.ViewModels
{
    public class SubFilter
    {
        [Required]
        public string Logic { get; set; }
        public List<FilterDefinition> Filters { get; set; }
    }
}