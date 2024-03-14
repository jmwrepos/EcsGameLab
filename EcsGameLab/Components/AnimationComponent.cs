using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public abstract class AnimationComponent : Component
    {
        protected AnimationComponent(bool expires) : base(expires)
        {
        }

        public bool HasStarted { get; set; }
        public bool HasFinished { get; set; }
        public int RenderOrder { get; set; }
        public double Duration { get; set; }
        public double StartTime { get; set; } = -1;

        // You might have common animation properties or methods here
        public virtual void StartAnimation(GameTime gameTime) { }
        public virtual void Reset()
        {
            StartTime = -1;
            HasFinished = false;
            HasStarted = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
