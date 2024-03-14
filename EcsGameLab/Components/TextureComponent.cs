using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EcsGameLab.Components
{
    public class TextureComponent : Component
    {
        public Texture2D Texture { get; private set; }
        public int Width => Texture.Width;
        public int Height => Texture.Height;

        public TextureComponent(GraphicsDevice graphicsDevice, Texture2D texture)
        {
            Texture = texture;
        }
    }
}
