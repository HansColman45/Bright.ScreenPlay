using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using Bright.ScreenPlay.Abilities;
using Bright.UnitTest.Tasks;

namespace Bright.UnitTest.Question
{
    public class TestQuestion : Question<string>
    {
        public override string PerformAs(IPerformer actor)
        {
            var api = actor.GetAbility<CallAnApi>();
            actor.Perform<TestTask>();
            return api.Settings.BaseUrl;
        }
    }
}
