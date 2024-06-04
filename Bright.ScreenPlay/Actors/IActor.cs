using Bright.ScreenPlay.Abilities;
using Bright.ScreenPlay.Questions;
using System.Resources;

namespace Bright.ScreenPlay.Actors
{
    public interface IActor: IPerformer
    {

    }
    public class Actor : IActor
    {
        readonly IAbilityStore abilityStore;
        readonly string name;

        public string Name => name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        /// <param name="name">The actor's name.</param>
        public Actor(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            this.name = name;
            abilityStore = new AbilityStore();
        }
        public void IsAbleTo<TAbility>() where TAbility : IAbility, new()
        {
            var ability = abilityStore.Add(typeof(TAbility));
            InvokeGainedAbility(ability);
        }
        public void IsAbleTo(Type abilityType)
        {
            var ability = abilityStore.Add(abilityType);
            InvokeGainedAbility(ability);
        }
        public void IsAbleTo(IAbility ability)
        {
            abilityStore.Add(ability);
            InvokeGainedAbility(ability);
        }
        protected void InvokeGainedAbility(IAbility ability)
        {
            var args = new GainAbilityEventArgs(this, ability);
        }
        #region IPerformable implementation
        bool IPerformer.HasAbility<TAbility>()
        {
            return HasAbility<TAbility>();
        }
        TAbility IPerformer.GetAbility<TAbility>()
        {
            return GetAbility<TAbility>();
        }
        void IPerformer.Perform(IPerformable performable)
        {
            Perform(performable);
        }

        void IPerformer.Perform<TPerformable>()
        {
            Perform<TPerformable>();
        }

        TResult IPerformer.Perform<TResult>(IPerformable<TResult> performable)
        {
            return Perform(performable);
        }
        public virtual bool HasAbility<TAbility>() where TAbility : IAbility
        {
            return abilityStore.HasAbility<TAbility>();
        }
        /// <summary>
        /// Performs an action or task.
        /// </summary>
        /// <param name="performable">The performable item to execute.</param>
        protected virtual void Perform(IPerformable performable)
        {
            if (ReferenceEquals(performable, null))
                throw new ArgumentNullException(nameof(performable));

            try
            {
                performable.PerformAs(this);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Performs an action or task which has a public parameterless constructor.
        /// </summary>
        /// <typeparam name="TPerformable">The type of the performable item to execute.</typeparam>
        protected virtual void Perform<TPerformable>() where TPerformable : IPerformable, new()
        {
            var performable = Activator.CreateInstance<TPerformable>();
            Perform(performable);
        }
        /// <summary>
        /// Performs an action, task or asks a question which returns a result value.
        /// </summary>
        /// <returns>The result of performing the item</returns>
        /// <param name="performable">The performable item to execute.</param>
        /// <typeparam name="TResult">The result type.</typeparam>
        protected virtual TResult Perform<TResult>(IPerformable<TResult> performable)
        {
            if (ReferenceEquals(performable, null))
                throw new ArgumentNullException(nameof(performable));

            TResult result;

            try
            {
                result = performable.PerformAs(this);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
        /// <summary>
        /// Gets an ability of the noted type.
        /// </summary>
        /// <returns>The ability.</returns>
        /// <typeparam name="TAbility">The desired ability type.</typeparam>
        public TAbility GetAbility<TAbility>() where TAbility : IAbility
        {
            var ability = abilityStore.GetAbility<TAbility>();

            if (ReferenceEquals(ability, null))
            {
                var message = String.Format(Name, typeof(TAbility).Name);
                throw new MissingAbilityException(message);
            }

            return ability;
        }
        #endregion
    }
}
