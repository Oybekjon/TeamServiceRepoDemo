﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTierApplication.Service;
using NTierApplication.Service.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace NTierApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
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
    [AllowAnonymous]
    public IActionResult GetItems( int offset, int limit )
    {

        if (offset < 0 || limit < 0)
            return BadRequest("Error");

        return Ok( ItemService.GetItemsByPagination( offset, limit ));
    }

    [HttpPost(Name = "CreateNew")]
    public ItemViewModelExtended CreateNew( [FromBody] ItemViewModelShort itemViewModel)
    {
        return ItemService.CreateNew(itemViewModel);
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
        return ItemService.Delete(id);
    }

    [HttpPut]
    public void Update(ItemViewModelExtended item)
    {
        ItemService.Update(item);
    }


}
