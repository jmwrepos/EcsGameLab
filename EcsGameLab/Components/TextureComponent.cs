using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EcsGameLab.Components
{
    public class TextureComponent : Component
    {
        public Texture2D Texture { get; private set; }
        public int Width => Texture.Width;
        public int Height => Texture.Height;

        public TextureComponent(GraphicsDevice graphicsDevice, Texture2D texture, Vector2 size)
        {
            Texture = ResizeTexture(graphicsDevice, texture, (int)size.X, (int)size.Y);
        }

        private Texture2D ResizeTexture(GraphicsDevice graphicsDevice, Texture2D originalTexture, int width, int height)
        {
            // Create a new RenderTarget2D with the desired size
            RenderTarget2D renderTarget = new RenderTarget2D(graphicsDevice, width, height);

            // Set the render target to our new RenderTarget2D
            graphicsDevice.SetRenderTarget(renderTarget);

            // Start drawing
            graphicsDevice.Clear(Color.Transparent);
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            spriteBatch.Begin();
            spriteBatch.Draw(originalTexture, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.End();

            // Reset the render target to the default
            graphicsDevice.SetRenderTarget(null);

            // Create a new Texture2D from the RenderTarget2D
            Texture2D resizedTexture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            renderTarget.GetData(data);
            resizedTexture.SetData(data);

            // Clean up
            renderTarget.Dispose();
            spriteBatch.Dispose();

            return resizedTexture;
        }
    }
}
