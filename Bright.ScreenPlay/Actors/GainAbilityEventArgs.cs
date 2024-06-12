using Bright.ScreenPlay.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bright.ScreenPlay.Actors
{
    public class GainAbilityEventArgs : ActorEventArgs
    {
        /// <summary>
        /// Gets the ability which was added to the actor.
        /// </summary>
        /// <value>The ability.</value>
        public IAbility Ability { get; private set; }
        public GainAbilityEventArgs(Actor actor, IAbility ability) : base(actor)
        {
            if (ability is null)
                throw new ArgumentNullException(nameof(ability));
            
            Ability = ability;
        }
    }
}
