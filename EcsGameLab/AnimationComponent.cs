using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public abstract class AnimationComponent : Component
    {
        public bool HasStarted { get; set; }
        public bool HasFinished { get; set; }
        public int RenderOrder { get; set; }
        public double Duration { get; set; }
        public double StartTime { get; set; } = -1;
        public AnimationComponent StartAfter { get; set; }

        // You might have common animation properties or methods here
        public virtual void StartAnimation(GameTime gameTime) { }
        public virtual void Reset()
        {
            StartTime = -1;
            HasFinished = false;
            HasStarted = false;
        }

        public virtual void SetStartAfter(AnimationComponent component)
        {
            StartAfter = component;
        }
    }
}
