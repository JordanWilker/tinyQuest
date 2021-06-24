using System;
using System.Collections.Generic;
using System.Linq;
using tinyQuest.Models;
using tinyQuest.Repositories;

namespace tinyQuest.Services
{
   public class PartiesService
   {
      private readonly PartiesRepository _repo;

      public PartiesService(PartiesRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Party> GetAll()
      {
         return _repo.GetAll();
      }

      internal Party GetById(int id)
      {
         Party data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Party Create(Party newParty)
      {
         return _repo.Create(newParty);
      }

     internal Party Edit(Party editData, string userId)
    {
      Party original = GetById(editData.Id);
      if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Party You did not Create"); }
      return _repo.Edit(editData);

    }

      internal String Delete(int id, string userId)
      {
         Party original = GetById(id);
         if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Party You did not Create"); }
         _repo.Remove(id);
         return "successfully deleted";
      }
   }
}