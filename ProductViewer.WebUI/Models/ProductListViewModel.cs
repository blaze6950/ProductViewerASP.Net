using System.Collections.Generic;

namespace ProductViewer.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string SearchValue { get; set; }
    }
}