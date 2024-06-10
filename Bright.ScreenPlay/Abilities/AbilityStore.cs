
namespace Bright.ScreenPlay.Abilities
{
    public class AbilityStore : IAbilityStore
    {
        private readonly List<IAbility> abilities;
        /// <summary>
        /// The default constructor
        /// </summary>
        public AbilityStore()
        {
            abilities = new List<IAbility>();
        }
        /// <summary>
        /// This will adds a new Ability 
        /// </summary>
        /// <param name="ability"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(IAbility ability)
        {
            if (ability == null)
            {
                throw new ArgumentNullException(nameof(ability));
            }
            abilities.Add(ability);
        }
        /// <summary>
        /// This will adds a new Ability based on its Type
        /// </summary>
        /// <param name="abilityType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
        /// <summary>
        /// This will return the ability
        /// </summary>
        /// <typeparam name="TAbility"></typeparam>
        /// <returns></returns>
        public TAbility GetAbility<TAbility>() where TAbility : IAbility
        {
            return (TAbility)abilities.FirstOrDefault(x => AbilityImplementsType(x, typeof(TAbility)));
        }
        /// <summary>
        /// Will check if the Actor has the ablity or not
        /// </summary>
        /// <typeparam name="TAbility"></typeparam>
        /// <returns></returns>
        public bool HasAbility<TAbility>() where TAbility : IAbility
        {
            return abilities.Any(x => AbilityImplementsType(x, typeof(TAbility)));
        }
        /// <summary>
        /// Disposes all elements
        /// </summary>
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
