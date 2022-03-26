using ConsoleGameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Animations
{
    /// <summary>
    /// Defines members required to be a target for an animation.
    /// </summary>
    /// <remarks>
    /// It is up to the implementor to make sure the animation actually plays.
    /// </remarks>
    public interface IAnimationTarget
    {
        /// <summary>
        /// The animation that is currently being played.
        /// </summary>
        Animation? CurrentAnimation { get; set; }
    }
}
