using Microsoft.AspNetCore.Mvc;
using NTierApplication.Service;
using NTierApplication.Service.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace NTierApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService ItemService;

        public ItemController(IItemService itemService)
        {
            ItemService = itemService;
        }

        [HttpGet]
        [Route("")]
        [SwaggerOperation(OperationId = "GetAll")]
        public ICollection<ItemViewModelExtended> GetAll()
        {
            return ItemService.GetItems();
        }

        [HttpPost(Name = "CreateNew")]
        public ItemViewModelExtended CreateNew(ItemViewModelExtended itemViewModel)
        {
            ItemService.CreateNew(itemViewModel);
            return itemViewModel;
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(OperationId = "GetById")]
        public ItemViewModelExtended GetById(long id)
        {
            return ItemService.GetById(id);
        }
    }
}
