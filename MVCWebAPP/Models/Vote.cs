namespace MVCWebAPP.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int mouseId { get; set; }
        public string userIp { get; set; }
        public DateTime DateTime { get; set; }
    }
}
