using Microsoft.EntityFrameworkCore;
using MVCWebAPP.Data;
using MVCWebAPP.Models;
using MVCWebAPP.Services.Interfaces;

//This code is how the website seperates the mice by the users preference.
namespace MVCWebAPP.Services
{
    public class MouseService : IMouseService
    
    {
        private readonly ApplicationDbContext _context;
        public MouseService(ApplicationDbContext context) { _context = context; }
        public async Task<List<Mouse>> GetAllMice()
        {
            List<Mouse> mice = new List<Mouse>();
            foreach(var m in _context.Mice) 
            {
                mice.Add(m);
            }
            return mice;
        }

        public async Task<List<Mouse>> GetMiceByPreference(MouseSearchViewModel model)
        {
            List<Mouse> mice = new List<Mouse>();
            foreach (var m in _context.Mice.Include(m => m.userVote))
            {
                if (model.IsWireless == m.IsWireless || model.IsWireless == null)
                {
                    if (model.Shape == m.Shape || model.Shape == null)
                    {
                        if (model.Size == m.Size || model.Size == null)
                        {
                            if (model.Weight >= m.Weight || model.Weight == null)
                            {
                                mice.Add(m);
                            }
                        }

                    }
                }
            }
            mice = mice.OrderBy(m => m.Rank == null).ThenBy(m => m.Rank).ToList();
            return mice;
        }
    }
}
