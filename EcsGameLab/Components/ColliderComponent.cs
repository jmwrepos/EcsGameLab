using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public class ColliderComponent : Component
    {
        public bool IsSolid { get; set; } = true;
        //if inveterted, collider system should only register a collision if the object is outside collider bounds
        public bool Invert { get; set; }
        public Rectangle Bounds { get; set; }
        public ColliderComponent(bool invert, Rectangle bounds, bool expires = false) : base(expires)
        {
            Invert = invert;
            Bounds = bounds;
            Name = $"{(Invert ? "Inverted " : string.Empty)}Collider";
        }
    }
}
