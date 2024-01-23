using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTierApplication.Service;
using NTierApplication.Service.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace NTierApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
//[Authorize]
public class ItemController : ControllerBase
{
    private readonly IItemService ItemService;

    public ItemController(IItemService itemService)
    {
        ItemService = itemService;
    }

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [SwaggerOperation(OperationId = "GetAll")]
    public ICollection<ItemViewModelExtended> GetAll()
    {
        return ItemService.GetItems();
    }

    [HttpGet]
    [Route("")]
    //[AllowAnonymous]
    public IActionResult GetItems( int offset, int limit )
    {

        if (offset < 0 || limit < 0)
            return BadRequest("Error");

        var res = ItemService.GetItemsByPagination(offset, limit);


        Thread.Sleep(200);
        return Ok( res);
    }

    [HttpPost(Name = "CreateNew")]
    public ItemViewModelExtended CreateNew( [FromBody] ItemViewModelShort itemViewModel)
    {
        var res = ItemService.CreateNew(itemViewModel);
        Thread.Sleep(400);
        return res;
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(OperationId = "GetById")]
    public ItemViewModelExtended GetById(long id)
    {
        return ItemService.GetById(id);
    }

    [HttpDelete]
    public int DeleteById(long id)
    {
        var res = ItemService.Delete(id);
        Thread.Sleep(200);
        return res;
    }

    [HttpPut]
    public void Update([FromBody]  ItemViewModelExtended item)
    {
        ItemService.Update(item);
    }


}
