using Microsoft.AspNetCore.Identity;

namespace MVCWebAPP.Models
{
    public class MouseUser : IdentityUser
    {
        public List<Mouse>? favoriteMice {get; set;}
    }
}
