using NTierApplication.DataAccess.Models;
using NTierApplication.Errors;
using NTierApplication.Repository;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service;

public class ItemService : IItemService
{
    /* Added readonly modifier */
    private readonly IItemRepository ItemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        ItemRepository = itemRepository;
    }

    public ItemPaginationViewModel GetItemsByPagination(ItemQuery query)
    {
        if (query.Limit > 100)
            query.Limit = 100;

        var result = new ItemPaginationViewModel();
        var dbQuery = ItemRepository.GetAll();
        if (query.SortDir == "desc")
        {
            dbQuery = SortDesc(query, dbQuery);
        }
        else
        {
            dbQuery = SortAsc(query, dbQuery);
        }

        if (query.Filter != null)
        {
            foreach (var item in query.Filter.Filters)
            {
                var isItemName = item.Filters.Any(x => x.Field == "itemName");
                if (isItemName)
                {
                    var containsFilter = item.Filters.FirstOrDefault(x => x.Operator == "contains");
                    if (containsFilter != null)
                    {
                        dbQuery = dbQuery.Where(x => x.ItemName.Contains(containsFilter.Value));
                    }
                }
            }
        }

        result.TotalCount = dbQuery.Count();

        result.ItemsPagination = dbQuery
            .Skip(query.Offset)
            .Take(query.Limit)
            .Select(x => new ItemViewModelExtended
            {
                ItemId = x.ItemId,
                ItemDate = x.ItemDate,
                ItemName = x.ItemName,
                ItemType = x.ItemType
            }).ToList();

        return result;
    }

    private static IQueryable<Item> SortAsc(ItemQuery query, IQueryable<Item> dbQuery)
    {
        return query.SortField switch
        {
            "itemName" => dbQuery.OrderBy(x => x.ItemName),
            "itemType" => dbQuery.OrderBy(x => x.ItemType),
            "itemDate" => dbQuery.OrderBy(x => x.ItemDate),
            _ => dbQuery.OrderBy(x => x.ItemId),
        };
    }

    private static IQueryable<Item> SortDesc(ItemQuery query, IQueryable<Item> dbQuery)
    {
        return query.SortField switch
        {
            "itemName" => dbQuery.OrderByDescending(x => x.ItemName),
            "itemType" => dbQuery.OrderByDescending(x => x.ItemType),
            "itemDate" => dbQuery.OrderByDescending(x => x.ItemDate),
            _ => dbQuery.OrderByDescending(x => x.ItemId),
        };
    }

    public ICollection<ItemViewModelExtended> GetItems()
    {
        return ItemRepository.GetAll().Select(x => new ItemViewModelExtended
        {
            ItemId = x.ItemId,
            ItemDate = x.ItemDate,
            ItemName = x.ItemName,
            ItemType = x.ItemType
        }).ToList();
    }


    public ItemViewModelExtended CreateNew(ItemViewModelShort item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        if (string.IsNullOrWhiteSpace(item.ItemName))
        {
            throw new ParameterInvalidException("ItemName cannot be empty");
        }
        if (item.ItemType < 0)
        {
            throw new ParameterInvalidException("Item type must be equal or greater than 0");
        }

        var entity = new Item
        {
            ItemDate = item.ItemDate,
            ItemName = item.ItemName,
            ItemType = item.ItemType
        };
        ItemRepository.Insert(entity);
        ItemRepository.SaveChanges();

        var newExt = new ItemViewModelExtended
        {
            ItemId = entity.ItemId,
            ItemName = entity.ItemName,
            ItemType = entity.ItemType,
            ItemDate = entity.ItemDate,
        };

        return newExt;
    }

    public int Delete(long itemId)
    {
        var itemEntity = ItemRepository.GetAll().FirstOrDefault(x => x.ItemId == itemId);
        if (itemEntity == null)
            return 0;

        ItemRepository.Delete(itemEntity);
        return ItemRepository.SaveChanges();
    }

    public ItemViewModelExtended GetById(long id)
    {
        var result = ItemRepository.GetAll()
            .Select(x => new ItemViewModelExtended
            {
                ItemId = x.ItemId,
                ItemDate = x.ItemDate,
                ItemName = x.ItemName,
                ItemType = x.ItemType
            })
            .FirstOrDefault(x => x.ItemId == id);

        if (result == null)
        {
            throw new EntryNotFoundException("No such item");
        }
        return result; /* Don't leave dead code */
    }

    /* Removed unnecessary lines */
    public void Update(ItemViewModelExtended item)
    {
        var itemEntity = ItemRepository.GetAll().FirstOrDefault(x => x.ItemId == item.ItemId);
        if (itemEntity == null)
            throw new EntryNotFoundException("No such item to update");

        itemEntity.ItemDate = item.ItemDate;
        itemEntity.ItemName = item.ItemName;
        itemEntity.ItemType = item.ItemType;
        ItemRepository.SaveChanges();
    }
}
