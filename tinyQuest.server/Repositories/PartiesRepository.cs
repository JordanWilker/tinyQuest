using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using tinyQuest.Models;

namespace tinyQuest.Repositories
{
    public class PartiesRepository
    {
        private readonly IDbConnection _db;

        public PartiesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Party> GetAll()
        {
            string sql = @"
      SELECT 
      party.*,
      prof.*
      FROM party party
      JOIN profiles prof ON party.creatorId = prof.id";
            return _db.Query<Party, Profile, Party>(sql, (Party, profile) =>
            {
                Party.Creator = profile;
                return Party;
            }, splitOn: "id");
        }

        internal Party GetById(int id)
        {
            string sql = @" 
      SELECT 
      party.*,
      prof.*
      FROM party party
      JOIN profiles prof ON party.creatorId = prof.id
      WHERE party.id = @id;";
            return _db.Query<Party, Profile, Party>(sql, (Party, profile) =>
            {
                Party.Creator = profile;
                return Party;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Party Create(Party newParty)
        {
            string sql = @"
         INSERT INTO party
         (hero1Id, hero2Id, hero3Id, hero4Id, creatorId)
         VALUES
         (@hero1Id, @hero2Id, @hero3Id, @hero4Id, @creatorId);
         SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newParty);
            newParty.Id = id;
            return newParty;
        }

        internal Party Edit(Party data)
        {
            string sql = @"
         UPDATE party
         SET
            hero1Id = @hero1Id,
            hero2Id = @hero2Id
            hero3Id = @hero3Id
            hero4Id = @hero4Id
         WHERE id = @id;
         SELECT * FROM party WHERE id = @id;";
            Party returnParty = _db.QueryFirstOrDefault<Party>(sql, data);
            return returnParty;
        }

        internal void Remove(int id)
        {
            string sql = "DELETE FROM party WHERE Id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

    }
}