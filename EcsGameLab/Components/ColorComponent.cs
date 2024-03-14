using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public class ColorComponent : Component
    {
        public Color Color { get; set; } = Color.White;

        public ColorComponent(Color color, bool expires = false) : base(expires)
        {
            Color = color;
        }
    }
}
