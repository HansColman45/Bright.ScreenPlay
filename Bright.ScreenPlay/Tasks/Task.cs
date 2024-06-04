using Bright.ScreenPlay.Actors;

namespace Bright.ScreenPlay.Tasks
{
    public interface ITask
    {
        void PerformAs(IActor actor);
        void AttemptsTo(ITask task);
    }
}
