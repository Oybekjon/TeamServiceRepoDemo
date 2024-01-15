namespace NTierApplication.Service.ViewModels;

public class ItemPaginationViewModel
{
    public int TotalCount { get; set; }

    public List<ItemViewModelExtended> ItemsPagination { get; set; }
}
