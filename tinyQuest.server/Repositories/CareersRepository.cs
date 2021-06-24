using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using tinyQuest.Models;

namespace tinyQuest.Repositories
{
    public class CareersRepository
    {
        private readonly IDbConnection _db;

        public CareersRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Career> GetAll()
        {
            string sql = @"
      SELECT 
      career.*,
      prof.*
      FROM career career
      JOIN profiles prof ON career.creatorId = prof.id";
            return _db.Query<Career, Profile, Career>(sql, (Career, profile) =>
            {
                Career.Creator = profile;
                return Career;
            }, splitOn: "id");
        }

        internal Career GetById(int id)
        {
            string sql = @" 
      SELECT 
      career.*,
      prof.*
      FROM career career
      JOIN profiles prof ON career.creatorId = prof.id
      WHERE career.id = @id;";
            return _db.Query<Career, Profile, Career>(sql, (Career, profile) =>
            {
                Career.Creator = profile;
                return Career;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Career Create(Career newCareer)
        {
            string sql = @"
         INSERT INTO career
         (name, healthMod, rangeMod, magicMod, swordMod, creatorId)
         VALUES
         (@name, @healthMod, @rangeMod, @magicMod, @swordMod, @creatorId);
         SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newCareer);
            newCareer.Id = id;
            return newCareer;
        }

        internal Career Edit(Career data)
        {
            string sql = @"
         UPDATE career
         SET
            name = @name,
            healthMod = @healthMod
            rangeMod = @rangeMod
            magicMod = @magicMod
            swordMod = @swordMod
         WHERE id = @id;
         SELECT * FROM career WHERE id = @id;";
            Career returnCareer = _db.QueryFirstOrDefault<Career>(sql, data);
            return returnCareer;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM career WHERE Id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

    }
}