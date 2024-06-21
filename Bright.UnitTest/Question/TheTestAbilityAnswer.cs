using Bright.ScreenPlay.Abilities;
using Bright.ScreenPlay.Actors;
using Bright.ScreenPlay.Questions;
using Bright.UnitTest.TestAbilities;

namespace Bright.UnitTest.Question
{
    public class TheTestAbilityAnswer : Question<TestAbility>
    {
        public override TestAbility PerformAs(IPerformer actor)
        {
            var api = actor.GetAbility<CallAnApi>();
            return new TestAbility();
        }
    }
}
