using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using tinyQuest.Models;

namespace tinyQuest.Repositories
{
   public class RacesRepository
   {
      private readonly IDbConnection _db;

      public RacesRepository(IDbConnection db)
      {
         _db = db;
      }

      internal IEnumerable<Race> GetAll()
       {
      string sql = @"
      SELECT 
      race.*,
      prof.*
      FROM race race
      JOIN profiles prof ON race.creatorId = prof.id";
      return _db.Query<Race, Profile, Race>(sql, (Race, profile) =>
      {
        Race.Creator = profile;
        return Race;
      }, splitOn: "id");
    }

      internal Race GetById(int id)
       {
      string sql = @" 
      SELECT 
      race.*,
      prof.*
      FROM race race
      JOIN profiles prof ON race.creatorId = prof.id
      WHERE race.id = @id;";
      return _db.Query<Race, Profile, Race>(sql, (Race, profile) =>
      {
        Race.Creator = profile;
        return Race;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }

      internal Race Create(Race newRace)
      {
         string sql = @"
         INSERT INTO race
         (name, healthMod, rangeMod, magicMod, swordMod, creatorId)
         VALUES
         (@name, @healthMod, @rangeMod, @magicMod, @swordMod, @creatorId);
         SELECT LAST_INSERT_ID();";
         int id = _db.ExecuteScalar<int>(sql, newRace);
         newRace.Id = id;
         return newRace;
      }

      internal Race Edit(Race data)
      {
         string sql = @"
         UPDATE race
         SET
            name = @name,
            healthMod = @healthMod
            rangeMod = @rangeMod
            magicMod = @magicMod
            swordMod = @swordMod
         WHERE id = @id;
         SELECT * FROM race WHERE id = @id;";
         Race returnRace = _db.QueryFirstOrDefault<Race>(sql, data);
         return returnRace;
      }

      internal void Remove(int id)
      {
         string sql = "DELETE FROM race WHERE Id = @id LIMIT 1";
         _db.Execute(sql, new { id });
      }

   }
}