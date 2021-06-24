using System;
using System.Collections.Generic;
using System.Linq;
using tinyQuest.Models;
using tinyQuest.Repositories;

namespace tinyQuest.Services
{
   public class HeroesService
   {
      private readonly HeroesRepository _repo;

      public HeroesService(HeroesRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Hero> GetAll()
      {
         return _repo.GetAll();
      }

      internal Hero GetById(int id)
      {
         Hero data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Hero Create(Hero newHero)
      {
         return _repo.Create(newHero);
      }

     internal Hero Edit(Hero editData, string userId)
    {
      Hero original = GetById(editData.Id);
      if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Hero You did not Create"); }
      editData.name= editData.name== null ? original.name: editData.name;
      return _repo.Edit(editData);

    }

      internal String Delete(int id, string userId)
      {
         Hero original = GetById(id);
         if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Hero You did not Create"); }
         _repo.Remove(id);
         return "successfully deleted";
      }
   }
}