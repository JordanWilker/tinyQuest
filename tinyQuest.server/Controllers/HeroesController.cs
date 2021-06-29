using System;
using Microsoft.AspNetCore.Mvc;
using tinyQuest.Models;
using tinyQuest.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Dapper;
using System.Data;
using System.Linq;


namespace tinyQuest.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class HeroesController : ControllerBase
   {
      private readonly HeroesService _service;
      private readonly IDbConnection _db;
      public HeroesController(HeroesService service, IDbConnection db)
      {
         _service = service;
         _db = db;
      }

      [HttpGet]
      public ActionResult<Hero> Get()
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
      public ActionResult<Hero> GetAll(int id)
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
      public async Task<ActionResult<Hero>> Post([FromBody] Hero newHero)
    {
      try
      {
        System.Random random = new System.Random();
        int raceNumber = random.Next(1,5);
        int careerNumber = random.Next(1,5);
        string raceQuery = @"
            SELECT * FROM race WHERE id = @Id;";
        var raceInfo = _db.QueryFirst<Race>(raceQuery, new { Id =  raceNumber});
        string careerQuery = @"
            SELECT * FROM career WHERE id = @Id;";
        var careerInfo = _db.QueryFirst<Career>(careerQuery, new {Id = careerNumber});
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newHero.creatorId = userInfo.Id;
        newHero.Creator = userInfo;
        newHero.name = careerInfo.name;
        newHero.raceId = raceInfo.Id;
        newHero.careerId = careerInfo.Id;
        newHero.health = raceInfo.healthMod + careerInfo.healthMod;
        newHero.rangePower = raceInfo.rangeMod + careerInfo.rangeMod;
        newHero.magicPower = raceInfo.magicMod + careerInfo.magicMod;
        newHero.swordPower = raceInfo.swordMod + careerInfo.swordMod;
        Hero created = _service.Create(newHero);

        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }     
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Hero>> Edit(int id, [FromBody] Hero editData)
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