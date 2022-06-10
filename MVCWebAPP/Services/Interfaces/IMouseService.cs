using MVCWebAPP.Models;

namespace MVCWebAPP.Services.Interfaces
{
    public interface IMouseService
    {
        public Task<List<Mouse>> GetAllMice();
        public Task<List<Mouse>> GetMiceByPreference(MouseSearchViewModel model);
    }

}
