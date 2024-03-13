using EcsGameLab.Components;
using Microsoft.Xna.Framework;
using System;
using EcsGameLab.Statics; // Assuming this is the namespace where AnimationEasing is defined

namespace EcsGameLab.Components
{
    public class ScaleAnimationComponent : Component
    {
        public Vector2 StartScale { get; set; }
        public Vector2 EndScale { get; set; }
        public double Duration { get; set; }
        private double elapsedTime;

        public bool Looping { get; set; } // Determines if the animation should loop
        public bool HasFinished { get; private set; }
        public EasingType EasingType { get; set; } // The type of easing to use
        public int RenderOrder { get; set; }
        public ScaleAnimationComponent(Vector2 startScale, Vector2 endScale, double duration, EasingType easingType, int renderOrder, bool looping = false)
        {
            StartScale = startScale;
            EndScale = endScale;
            Duration = duration;
            EasingType = easingType;
            Looping = looping;
            RenderOrder = renderOrder;
            Reset(); // Initialize the component state
        }

        public void Update(GameTime gameTime)
        {
            if (!HasFinished || Looping)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                double rawProgress = Math.Min(1, elapsedTime / Duration);

                // Get the easing function based on the specified EasingType
                Func<float, float> easingFunction = AnimationEasing.GetEasingFunction(EasingType);
                float progress = easingFunction((float)rawProgress);

                // Apply the easing function to interpolate between start and end scales
                Vector2 currentScale = Vector2.Lerp(StartScale, EndScale, progress);
                Owner.GetComponent<TransformComponent>().Scale = currentScale;

                if (rawProgress >= 1)
                {
                    if (Looping)
                    {
                        Reset(); // Reset for looping
                    }
                    else
                    {
                        HasFinished = true;
                    }
                }
            }
        }

        public void Reset()
        {
            elapsedTime = 0;
            HasFinished = false;
        }
    }
}