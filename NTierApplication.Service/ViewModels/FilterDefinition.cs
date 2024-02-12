using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NTierApplication.Service.ViewModels
{
    public class FilterDefinition
    {
        [Required]
        public string Field { get; set; }
        [Required]
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}