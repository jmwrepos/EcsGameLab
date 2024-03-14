using Microsoft.Xna.Framework;
using System;

namespace EcsGameLab.Components
{
    public class FadeAnimationComponent : AnimationComponent
    {
        public Color StartColor { get; set; }
        public Color EndColor { get; set; }
        public FadeAnimationComponent(string name, Color startColor, Color endColor, double duration, bool expires = true) : base(expires, name)
        {
            StartColor = startColor;
            EndColor = endColor;
            Duration = duration;
            Reset();
        }
        public override void Update(GameTime gameTime)
        {
            if(HasStarted && !HasFinished)
            {
                double elapsedTime = gameTime.TotalGameTime.TotalSeconds - StartTime;
                double progress = Math.Min(1, elapsedTime / Duration);
                Color currentColor = Color.Lerp(StartColor, EndColor, (float)progress);
                var colorComponent = Owner?.GetComponent<ColorComponent>();
                if (colorComponent != null)
                {
                    colorComponent.Color = currentColor;
                }

                if (progress >= 1)
                {
                    HasFinished = true;
                    EscLabLogger.Log($"Ending Animation for {Owner.Name}");
                }
            }
        }

        public override void StartAnimation(GameTime gameTime)
        {
            if (StartTime < 0) // Check if the animation has not started yet
            {
                StartTime = gameTime.TotalGameTime.TotalSeconds;
                HasStarted = true;
            }
        }

    }
}
