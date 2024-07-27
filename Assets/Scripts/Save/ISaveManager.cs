using System.Threading.Tasks;

namespace MiniIT.ARKANOID.Save
{
    public interface ISaveManager
    {
        public Task CheckSave();
        public Task SaveMaxScore(int value);
        public Task SaveSoundState(bool value);
        public Task<int> GetMaxScore();
        public Task<bool> GetSoundState();
    }
}