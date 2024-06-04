using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Settings;

namespace Bright.ScreenPlay.Abilities
{
    public interface IAbility: IDisposable
    {
        public ScreenPlaySettings Settings { get; set; }
    }
    public class Ability : IAbility
    {   
        public ScreenPlaySettings Settings { get; set; }
        public void As(IActor actor)
        {
            Actor = actor;
            return ;
        }

        public IActor Actor { get; set; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
