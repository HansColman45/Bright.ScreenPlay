using Bright.ScreenPlay.Actors;

namespace Bright.ScreenPlay.Questions
{
    public  interface IPerformableWithResult : IPerformable
    {
        new object PerformAs(IPerformer actor);
    }
}
