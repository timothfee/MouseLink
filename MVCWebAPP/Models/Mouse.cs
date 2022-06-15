using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
//This is the mouse class and what we use to construct the mouse as well as creat the UserVote for each mouse.
namespace MVCWebAPP.Models
{
    public class Mouse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Shape { get; set; }

        public int Weight { get; set; }

        [DisplayName("Wireless")]
        public bool IsWireless { get; set; }

        public int? Rank { get; set; }

        public MouseSize Size { get; set; }

        public string? URL { get; set; }
        public List<MouseUser>? userVote { get; set; }

    }
}
