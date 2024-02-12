using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Service.ViewModels
{
    public class ItemQuery
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public Filter Filter { get; set; }
        public string SortField { get; set; }
        public string SortDir { get; set; }
    }
}
