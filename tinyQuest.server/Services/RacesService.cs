using System;
using System.Collections.Generic;
using System.Linq;
using tinyQuest.Models;
using tinyQuest.Repositories;

namespace tinyQuest.Services
{
   public class RacesService
   {
      private readonly RacesRepository _repo;

      public RacesService(RacesRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Race> GetAll()
      {
         return _repo.GetAll();
      }

      internal Race GetById(int id)
      {
         Race data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Race Create(Race newRace)
      {
         return _repo.Create(newRace);
      }

     internal Race Edit(Race editData, string userId)
    {
      Race original = GetById(editData.Id);
      if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Race You did not Create"); }
      editData.name= editData.name== null ? original.name: editData.name;
      return _repo.Edit(editData);

    }

      internal String Delete(int id, string userId)
      {
         Race original = GetById(id);
         if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Race You did not Create"); }
         _repo.Remove(id);
         return "successfully deleted";
      }
   }
}