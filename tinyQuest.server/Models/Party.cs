namespace tinyQuest.Models
{
    public class Party{

            public Party()
        {
        }

        public int Id {get; set;}
        public string creatorId {get; set;}
        public int hero1Id {get; set;}
        public int hero2Id {get; set;}
        public int hero3Id {get; set;}
        public int hero4Id {get;set;}
        public Profile Creator {get; set;}

    }
}