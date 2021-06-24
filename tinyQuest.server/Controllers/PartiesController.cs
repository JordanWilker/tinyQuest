using System;
using Microsoft.AspNetCore.Mvc;
using tinyQuest.Models;
using tinyQuest.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;

namespace tinyQuest.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class PartiesController : ControllerBase
   {
      private readonly PartiesService _service;
      public PartiesController(PartiesService service)
      {
         _service = service;
      }

      [HttpGet]
      public ActionResult<Party> Get()
      {
         try
         {
            return Ok(_service.GetAll());
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpGet("{id}")]
      public ActionResult<Party> GetAll(int id)
      {
         try
         {
            return Ok(_service.GetById(id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }

      [HttpPost]
      [Authorize]
      public async Task<ActionResult<Party>> Post([FromBody] Party newParty)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newParty.creatorId = userInfo.Id;
        newParty.Creator = userInfo;
        Party created = _service.Create(newParty);

        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }     
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Party>> Edit(int id, [FromBody] Party editData)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        editData.Id = id;
        editData.Creator = userInfo;
        editData.creatorId = userInfo.Id;
        return Ok(_service.Edit(editData, userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
      [HttpDelete("{id}")]
      [Authorize]
      public async Task<ActionResult<string>> Delete(int id)
      {
         try
         {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            return Ok(_service.Delete(id, userInfo.Id));
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }
   }
}