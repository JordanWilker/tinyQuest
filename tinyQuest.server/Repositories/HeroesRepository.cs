using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using tinyQuest.Models;

namespace tinyQuest.Repositories
{
    public class HeroesRepository
    {
        private readonly IDbConnection _db;

        public HeroesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Hero> GetAll()
        {
            string sql = @"
      SELECT 
      hero.*,
      prof.*
      FROM hero hero
      JOIN profiles prof ON hero.creatorId = prof.id";
            return _db.Query<Hero, Profile, Hero>(sql, (Hero, profile) =>
            {
                Hero.Creator = profile;
                return Hero;
            }, splitOn: "id");
        }

        internal Hero GetById(int id)
        {
            string sql = @" 
      SELECT 
      hero.*,
      prof.*
      FROM hero hero
      JOIN profiles prof ON hero.creatorId = prof.id
      WHERE hero.id = @id;";
            return _db.Query<Hero, Profile, Hero>(sql, (Hero, profile) =>
            {
                Hero.Creator = profile;
                return Hero;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Hero Create(Hero newHero)
        {
            string sql = @"
         INSERT INTO hero
         (name, health, rangePower, magicPower, swordPower, creatorId, raceId, careerId)
         VALUES
         ('Mr.Rando1', @health, @rangePower, @magicPower, @swordPower, @creatorId, @raceId, @careerId);
         SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newHero);
            newHero.Id = id;
            return newHero;
        }

        internal Hero Edit(Hero data)
        {
            string sql = @"
         UPDATE hero
         SET
            name = @name,
            health = @health
            rangePower = @rangePower
            magicPower = @magicPower
            swordPower = @swordPower
            careerId = @careerId
            raceId = @raceId
         WHERE id = @id;
         SELECT * FROM hero WHERE id = @id;";
            Hero returnHero = _db.QueryFirstOrDefault<Hero>(sql, data);
            return returnHero;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM hero WHERE Id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

    }
}