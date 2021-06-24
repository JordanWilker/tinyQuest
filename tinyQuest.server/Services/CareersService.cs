using System;
using System.Collections.Generic;
using System.Linq;
using tinyQuest.Models;
using tinyQuest.Repositories;

namespace tinyQuest.Services
{
   public class CareersService
   {
      private readonly CareersRepository _repo;

      public CareersService(CareersRepository repo)
      {
         _repo = repo;
      }

      internal IEnumerable<Career> GetAll()
      {
         return _repo.GetAll();
      }

      internal Career GetById(int id)
      {
         Career data = _repo.GetById(id);
         if (data == null)
         {
            throw new Exception("Invalid Id");
         }
         return data;
      }

      internal Career Create(Career newCareer)
      {
         return _repo.Create(newCareer);
      }

     internal Career Edit(Career editData, string userId)
    {
      Career original = GetById(editData.Id);
      if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Edit a Career You did not Create"); }
      editData.name= editData.name== null ? original.name: editData.name;
      return _repo.Edit(editData);

    }

      internal String Delete(int id, string userId)
      {
         Career original = GetById(id);
         if (original.creatorId != userId) { throw new Exception("Access Denied: Cannot Delete a Career You did not Create"); }
         _repo.Remove(id);
         return "successfully deleted";
      }
   }
}