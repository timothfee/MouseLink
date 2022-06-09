using System.ComponentModel;

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

    }
}
