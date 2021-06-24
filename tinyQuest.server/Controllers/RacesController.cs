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
   public class RacesController : ControllerBase
   {
      private readonly RacesService _service;
      public RacesController(RacesService service)
      {
         _service = service;
      }

      [HttpGet]
      public ActionResult<Race> Get()
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
      public ActionResult<Race> GetAll(int id)
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
      public async Task<ActionResult<Race>> Post([FromBody] Race newRace)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newRace.creatorId = userInfo.Id;
        newRace.Creator = userInfo;
        Race created = _service.Create(newRace);

        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }     
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Race>> Edit(int id, [FromBody] Race editData)
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