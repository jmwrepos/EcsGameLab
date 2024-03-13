using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcsGameLab.Statics
{

    public static class AnimationEasing
    {
        // Linear Easing: no acceleration, no deceleration
        public static float Linear(float t) => t;

        // Quadratic Easing: accelerating from zero velocity
        public static float EaseInQuad(float t) => t * t;

        // Quadratic Easing: decelerating to zero velocity
        public static float EaseOutQuad(float t) => t * (2 - t);

        // Quadratic Easing: acceleration until halfway, then deceleration
        public static float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

        // Cubic Easing: accelerating from zero velocity
        public static float EaseInCubic(float t) => t * t * t;

        // Cubic Easing: decelerating to zero velocity
        public static float EaseOutCubic(float t) => (--t) * t * t + 1;

        // Adds more easing functions as needed...

        // Generic method to get easing function by type
        public static Func<float, float> GetEasingFunction(EasingType type)
        {
            switch (type)
            {
                case EasingType.Linear:
                    return Linear;
                case EasingType.EaseInQuad:
                    return EaseInQuad;
                case EasingType.EaseOutQuad:
                    return EaseOutQuad;
                case EasingType.EaseInOutQuad:
                    return EaseInOutQuad;
                case EasingType.EaseInCubic:
                    return EaseInCubic;
                case EasingType.EaseOutCubic:
                    return EaseOutCubic;
                // Add cases for other easing types
                default:
                    return Linear; // Fallback to linear if type is unknown
            }
        }
    }

    // Enum to represent the type of easing function
    public enum EasingType
    {
        Linear,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic,
        // Add more easing types as needed
    }
}
