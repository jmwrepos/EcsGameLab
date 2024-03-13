using Microsoft.Xna.Framework;
using System;

namespace EcsGameLab.Components
{
    public class FadeAnimationComponent : Component
    {
        public Color StartColor { get; set; } // Start color
        public Color EndColor { get; set; } // Target color
        public double Duration { get; set; }
        public double StartTime { get; set; } = -1; // Initialize with -1 to indicate animation hasn't started
        public bool HasFinished { get; private set; }
        public int RenderOrder { get; set; }

        public FadeAnimationComponent(Color startColor, Color endColor, double duration, int renderOrder)
        {
            StartColor = startColor;
            EndColor = endColor;
            Duration = duration;
            RenderOrder = renderOrder;
            Reset();
        }

        public void Update(GameTime gameTime)
        {
            if (StartTime < 0) return; // Animation not started yet

            double elapsedTime = gameTime.TotalGameTime.TotalSeconds - StartTime;
            double progress = Math.Min(1, elapsedTime / Duration); // Ensure progress does not exceed 1

            // Interpolate between StartColor and EndColor based on the progress
            Color currentColor = Color.Lerp(StartColor, EndColor, (float)progress);

            var colorComponent = Owner?.GetComponent<ColorComponent>();
            if (colorComponent != null)
            {
                colorComponent.Color = currentColor;
            }

            if (progress >= 1)
            {
                HasFinished = true;
            }
        }

        public void StartAnimation(GameTime gameTime)
        {
            if (StartTime < 0) // Animation not yet started
            {
                StartTime = gameTime.TotalGameTime.TotalSeconds;
            }
        }

        public void Reset()
        {
            StartTime = -1;
            HasFinished = false;
        }
    }
}
