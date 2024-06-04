using Bright.ScreenPlay.Actors;

namespace Bright.ScreenPlay.Questions
{
    public abstract class Performable : IPerformable
    {
        /// <summary>
        /// Performs this operation, as the given actor.
        /// </summary>
        /// <param name="actor">The actor performing this task.</param>
        public abstract void PerformAs(IPerformer actor);

        void IPerformable.PerformAs(IPerformer actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            PerformAs(actor);
        }
    }
    public abstract class Performable<TResult> : IPerformable<TResult>
    {
        /// <summary>
        /// Performs this operation, as the given actor.
        /// </summary>
        /// <returns>The response or result.</returns>
        /// <param name="actor">The actor performing this task.</param>
        public abstract TResult PerformAs(IPerformer actor);

        void IPerformable.PerformAs(IPerformer actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            PerformAs(actor);
        }

        object IPerformableWithResult.PerformAs(IPerformer actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            return PerformAs(actor);
        }

        TResult IPerformable<TResult>.PerformAs(IPerformer actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            return PerformAs(actor);
        }
    }
}
