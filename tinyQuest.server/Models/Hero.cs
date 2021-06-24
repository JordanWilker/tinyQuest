namespace tinyQuest.Models
{
    public class Hero
    {
         public Hero()
      {
      }

      public int Id { get; set; }
      public string creatorId {get;set;}
      public string name { get; set; }
      public int raceId {get;set;}
      public int careerId {get;set;}
      public int health {get;set;}
      public int rangePower {get;set;}
      public int magicPower {get;set;}
      public int swordPower {get;set;}
      public Profile Creator{get;set;}
    }
}