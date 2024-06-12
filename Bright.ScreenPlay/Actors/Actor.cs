using Bright.ScreenPlay.Abilities;
using Bright.ScreenPlay.Questions;

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
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            this.name = name;
            abilityStore = new AbilityStore();
        }
        /// <summary>
        /// This will give the actor a new Ability
        /// </summary>
        /// <typeparam name="TAbility"></typeparam>
        public void IsAbleToDoOrUse<TAbility>() where TAbility : IAbility, new()
        {
            var ability = abilityStore.Add(typeof(TAbility));
            InvokeGainedAbility(ability);
        }
        /// <summary>
        /// This will give the actor a new Ability
        /// </summary>
        /// <param name="abilityType"></param>
        public void IsAbleToDoOrUse(Type abilityType)
        {
            var ability = abilityStore.Add(abilityType);
            InvokeGainedAbility(ability);
        }
        /// <summary>
        /// This will give the actor a new Ability
        /// </summary>
        /// <param name="abilityType"></param>
        public void IsAbleToDoOrUse(IAbility ability)
        {
            abilityStore.Add(ability);
            InvokeGainedAbility(ability);
        }
        protected void InvokeGainedAbility(IAbility ability)
        {
            _ = new GainAbilityEventArgs(this, ability);
        }
        /// <summary>
        /// This will set a new Ability
        /// </summary>
        /// <param name="ability"></param>
        protected void SetNewAbility(IAbility ability)
        {
            abilityStore.Add(ability);
            InvokeGainedAbility(ability);
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
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAbility"></typeparam>
        /// <returns></returns>
        public virtual bool HasAbility<TAbility>() where TAbility : IAbility
        {
            return abilityStore.HasAbility<TAbility>();
        }
        /// <summary>
        /// Performs an action or task.
        /// </summary>
        /// <param name="performable">The performable item to execute.</param>
        protected void Perform(IPerformable performable)
        {
            if (performable is null)
                throw new ArgumentNullException(nameof(performable));

            try
            {
                performable.PerformAs(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Performs an action or task which has a public parameterless constructor.
        /// </summary>
        /// <typeparam name="TPerformable">The type of the performable item to execute.</typeparam>
        public void Perform<TPerformable>() where TPerformable : IPerformable, new()
        {
            var performable = Activator.CreateInstance<TPerformable>();
            Perform(performable);
        }
        /// <summary>
        /// Performs a question which returns a result value.
        /// </summary>
        /// <typeparam name="TQuestion"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="question"></param>
        public void TResult<TQuestion, TResult>(TQuestion question) where TQuestion : Question<TResult>
        {
            Perform(question);
        }
        /// <summary>
        /// Performs an action, task or asks a question which returns a result value.
        /// </summary>
        /// <returns>The result of performing the item</returns>
        /// <param name="performable">The performable item to execute.</param>
        /// <typeparam name="TResult">The result type.</typeparam>
        protected virtual TResult Perform<TResult>(IPerformable<TResult> performable)
        {
            if (performable is null)
                throw new ArgumentNullException(nameof(performable));

            TResult result;

            try
            {
                result = performable.PerformAs(this);
            }
            catch (Exception)
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

            if (ability is null)
            {
                var message = String.Format(Name, typeof(TAbility).Name);
                throw new MissingAbilityException(message);
            }

            return ability;
        }
        /// <summary>
        /// Will set a new abillity
        /// </summary>
        /// <param name="ability"></param>
        void IPerformer.SetAbility(IAbility ability)
        {
            SetNewAbility(ability);
        }
        #endregion
    }
}
