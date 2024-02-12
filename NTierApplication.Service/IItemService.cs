using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public interface IItemService
{
    ItemViewModelExtended CreateNew(ItemViewModelShort item);
    void Update(ItemViewModelExtended item);
    int Delete(long itemId);
    ICollection<ItemViewModelExtended> GetItems();
    ItemPaginationViewModel GetItemsByPagination(ItemQuery query);
    ItemViewModelExtended GetById(long id);
}
