using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Service.ViewModels
{
    public class Filter
    {
        private List<SubFilter> filters;

        [Required]
        public string Logic { get; set; }
        public List<SubFilter> Filters
        {
            get => filters ??= new List<SubFilter>();
            set => filters = value;
        }
    }
}
