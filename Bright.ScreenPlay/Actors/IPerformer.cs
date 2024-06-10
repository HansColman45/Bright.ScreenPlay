using Bright.ScreenPlay.Abilities;
using Bright.ScreenPlay.Questions;

namespace Bright.ScreenPlay.Actors
{
    public interface IPerformer
    {
        bool HasAbility<TAbility>() where TAbility : IAbility;
        TAbility GetAbility<TAbility>() where TAbility : IAbility;
        void SetAbility(IAbility ability);
        void Perform(IPerformable performable); 
        void Perform<TPerformable>() where TPerformable : IPerformable, new();
        TResult Perform<TResult>(IPerformable<TResult> performable);
    }
}
