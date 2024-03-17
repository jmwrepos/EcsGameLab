using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public class VelocityComponent : Component
    {
        public Vector2 Velocity { get; set; }
        public VelocityComponent(bool expires = false) : base(expires)
        {
            Velocity = Vector2.Zero;
        }
    }
}
