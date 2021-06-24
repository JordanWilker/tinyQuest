namespace tinyQuest.Models
{
    public class Race{
        public Race()
        {
        }
        public int Id{get;set;}
        public string creatorId{get;set;}
        public string name{get;set;}
        public int healthMod{get;set;}
        public int rangeMod{get;set;}
        public int magicMod{get;set;}
        public int swordMod{get;set;}
        public Profile Creator{get;set; }
    }
}