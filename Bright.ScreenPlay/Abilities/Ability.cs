using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Settings;

namespace Bright.ScreenPlay.Abilities
{
    public interface IAbility: IDisposable
    {
        /// <summary>
        /// The settings
        /// </summary>
        public ScreenPlaySettings Settings { get; set; }
    }
    public class Ability : IAbility
    {   
        public ScreenPlaySettings Settings { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public Ability()
        {
            Settings = new();
        }
        protected void Dispose()
        {
            Settings = null;
        }
        /// <summary>
        /// The Dispose function
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}
