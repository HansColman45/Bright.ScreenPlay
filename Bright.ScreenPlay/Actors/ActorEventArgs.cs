namespace Bright.ScreenPlay.Actors
{
    public class ActorEventArgs: EventArgs
    {
        /// <summary>
        /// Gets the actor.
        /// </summary>
        /// <value>The actor</value>
        public Actor Actor { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Actor class.
        /// </summary>
        /// <param name="actor">Actor.</param>
        public ActorEventArgs(Actor actor)
        {
            if (actor is null)
                throw new ArgumentNullException(nameof(actor));

            Actor = actor;
        }
    }
}
