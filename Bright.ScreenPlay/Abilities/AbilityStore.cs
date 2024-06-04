
namespace Bright.ScreenPlay.Abilities
{
    public class AbilityStore : IAbilityStore
    {
        private readonly List<IAbility> abilities;

        public AbilityStore()
        {
            abilities = new List<IAbility>();
        }
        public void Add(IAbility ability)
        {
            if (ability == null)
            {
                throw new ArgumentNullException(nameof(ability));
            }
            abilities.Add(ability);
        }

        public IAbility Add(Type abilityType)
        {
            if(!typeof(IAbility).IsAssignableFrom(abilityType))
            {
                throw new ArgumentException($"The type {abilityType} does not implement {typeof(IAbility)}");
            }
            var ability = (IAbility)Activator.CreateInstance(abilityType);
            Add(ability);
            return ability;
        }

        public TAbility GetAbility<TAbility>() where TAbility : IAbility
        {
            return (TAbility)abilities.FirstOrDefault(x => AbilityImplementsType(x, typeof(TAbility)));
        }

        public bool HasAbility<TAbility>() where TAbility : IAbility
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            foreach (var ability in abilities)
            {
                ability.Dispose();
            }
        }
        private bool AbilityImplementsType(IAbility ability, Type desiredType)
        {
            var abilityType = GetAbilityType(ability);
            if (abilityType == null)
                return false;

            return desiredType.IsAssignableFrom(abilityType);
        }
        private Type GetAbilityType(IAbility ability)
        {
            if (ReferenceEquals(ability, null))
                return null;

            return ability.GetType();
        }
    }
}
