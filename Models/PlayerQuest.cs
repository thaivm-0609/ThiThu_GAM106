namespace ThiThu.Models
{
    public class PlayerQuest
    {
        public int playerID {  get; set; }
        public int questID { get; set; }
        public bool completionStatus { get; set; }

        public Quest Quest { get; set; }
    }
}
