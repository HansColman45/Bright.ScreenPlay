namespace Bright.ScreenPlay.Abilities
{
    public interface IAbilityStore :IDisposable
    {
        /// <summary>
        /// Determines whether or not the current instance contains an ability or not.
        /// </summary>
        /// <returns><c>true</c>, if the current instance has the ability, <c>false</c> otherwise.</returns>
        /// <typeparam name="TAbility">The desired ability type.</typeparam>
        public bool HasAbility<TAbility>() where TAbility : IAbility;

        /// <summary>
        /// Gets an ability of the noted type.
        /// </summary>
        /// <returns>The ability.</returns>
        /// <typeparam name="TAbility">The desired ability type.</typeparam>
        public TAbility GetAbility<TAbility>() where TAbility : IAbility;

        /// <summary>
        /// Adds an ability to the current instance.
        /// </summary>
        /// <param name="ability">The ability.</param>
        public void Add(IAbility ability);

        /// <summary>
        /// Instantiates an ability of the given type, adds it to the current store instance and returns the created ability.
        /// </summary>
        /// <param name="abilityType">The ability type.</param>
        public IAbility Add(Type abilityType);
    }
}
