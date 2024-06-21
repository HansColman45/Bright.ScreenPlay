using Bright.ScreenPlay.Abilities;

namespace Bright.UnitTest.TestAbilities
{
    public class TestAbility: Ability
    {
        public TestAbility()
        {
            WebUrl = "Test";
        }
        public string WebUrl { get; set; }
    }
}
