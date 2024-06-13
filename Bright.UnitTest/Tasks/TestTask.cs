using Bright.ScreenPlay.Abilities;
using Bright.ScreenPlay.Actors;
using Task = Bright.ScreenPlay.Tasks.Task;

namespace Bright.UnitTest.Tasks
{
    public class TestTask : Task
    {
        public override void PerformAs(IPerformer actor)
        {
            var test = actor.GetAbility<CallAnApi>();
            test.Settings.BaseUrl = "Test";
        }
    }
}
