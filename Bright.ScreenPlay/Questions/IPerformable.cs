using Bright.ScreenPlay.Abilities;
using Bright.ScreenPlay.Actors;

namespace Bright.ScreenPlay.Questions
{
    public interface IPerformable
    {
        /// <summary>
        /// Performs this operation, as the given actor.
        /// </summary>
        /// <param name="actor">The actor performing this task.</param>
        void PerformAs(IPerformer actor);
    }
    public interface IPerformable<TResult> : IPerformableWithResult
    {
        new TResult PerformAs(IPerformer actor);
    }
}
