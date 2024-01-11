using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public interface IItemService
{
    ItemViewModelExtended CreateNew(ItemViewModelShort item);
    void Update(ItemViewModelExtended item);
    void Delete(long itemId);
    ICollection<ItemViewModelExtended> GetItems();
    ItemViewModelExtended GetById(long id);
}
