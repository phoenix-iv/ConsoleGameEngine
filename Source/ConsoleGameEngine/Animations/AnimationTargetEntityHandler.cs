using ConsoleGameEngine.Components;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Animations
{
    /// <summary>
    /// A helper class that provides functionality for an <see cref="IAnimationTarget"/>
    /// </summary>
    public class AnimationTargetEntityHandler
    {
        private readonly Entity _entity;

        /// <summary>
        /// Creates a new instance of <see cref="AnimationTargetEntityHandler"/>
        /// </summary>
        /// <param name="entity"></param>
        public AnimationTargetEntityHandler(Entity entity)
        {
            _entity = entity;
        }

        /// <summary>
        /// Gets the Animation component from the entity, or null if the entity does not have an Animation component.
        /// </summary>
        /// <returns>The Animation component, or null.</returns>
        public Animation? GetCurrentAnimation()
        {
            if (_entity.Has<Animation>())
                return _entity.Get<Animation>();
            return null;
        }

        /// <summary>
        /// Sets or removes the wrapped entities Animation component based on the specified value.
        /// </summary>
        /// <param name="value">The animation.</param>
        public void SetCurrentAnimation(Animation? value)
        {
            if (value == null)
                _entity.Remove<Animation>();
            else
                _entity.Set(value.Value);
        }
    }
}
