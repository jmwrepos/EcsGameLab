using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public class TransformComponent : Component
    {
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Radius { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One; // Default scale is 1
    }
}
