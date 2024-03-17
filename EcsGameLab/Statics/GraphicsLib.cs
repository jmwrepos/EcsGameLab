using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace EcsGameLab.Statics
{
    public static class GraphicsLib
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;

        public static Vector2 AbsMiddle => GetDisplaySize / 2;
        public static Vector2 DesignSize = new Vector2(1920, 1080);
        public static Vector2 GetDisplaySize => new(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        public static void SetManagers(ContentManager contentManager, GraphicsDeviceManager graphicsDeviceManager) => (_content, _graphics) = (contentManager, graphicsDeviceManager);
        public static void SetPixel()
        {
            Pixel = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }
        public static SpriteFont GetFont(string name) => _content?.Load<SpriteFont>(FontRoot + name);
        public static Texture2D GetStatic(string name) => _content?.Load<Texture2D>(StaticsRoot + name);
        public static Texture2D GetAnimation(string name) => _content?.Load<Texture2D>(AnimationsRoot + name);
        public static Texture2D Pixel { get; private set; }
        public static Texture2D MakeTextureFromText(string fontName, string text, Color fore, Color back)
        {
            var font = _content.Load<SpriteFont>(FontRoot + fontName);
            var textSize = font.MeasureString(text);

            // Create a RenderTarget to draw the text onto
            RenderTarget2D renderTarget = new RenderTarget2D(
                _graphics.GraphicsDevice,
                (int)textSize.X,
                (int)textSize.Y);

            _graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            _graphics.GraphicsDevice.Clear(back);

            // Begin drawing
            SpriteBatch spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, Vector2.Zero, fore);
            spriteBatch.End();

            // Reset the render target
            _graphics.GraphicsDevice.SetRenderTarget(null);

            return renderTarget;
        }

        public static Rectangle GetScaledAndCenteredRectangle(int originalWidth, int originalHeight)
        {
            // Current display size
            int displayWidth = _graphics.GraphicsDevice.PresentationParameters.BackBufferWidth;
            int displayHeight = _graphics.GraphicsDevice.PresentationParameters.BackBufferHeight;

            // Calculate scale factors
            float scaleX = (float)displayWidth / DesignSize.X;
            float scaleY = (float)displayHeight / DesignSize.Y;
            float scale = Math.Min(scaleX, scaleY); // Maintains aspect ratio

            // Scale the rectangle size
            int scaledWidth = (int)(originalWidth * scale);
            int scaledHeight = (int)(originalHeight * scale);

            // Center the rectangle
            int xPosition = (displayWidth - scaledWidth) / 2;
            int yPosition = (displayHeight - scaledHeight) / 2;

            // Create and return the scaled and centered rectangle
            return new Rectangle(xPosition, yPosition, scaledWidth, scaledHeight);
        }

        public static string FontRoot = "fonts/";
        public static string StaticsRoot = "statics/";
        public static string AnimationsRoot = "anim/";

        //FONTS        
        public static class FontPaths
        {
            public static string MainMenuTitle = "gameTitle";
            public static string MainMenuOption = "mainMenuOption";
        }

        //STATICS
        public static class StaticsPaths
        {
            public static string Circ30x30 = "circ_30x30";
            public static string MainMenuNew = "mainMenuNew";
            public static string MainMenuBlueRect = "mainMenuTextureRect";
            public static string Node001 = "node001";
            public static string Dial = "dial";
        }
        
        //ANIMATIONS
        public static class AnimationPaths
        {
            public static string CircleGrow = "circle_grow";
            public static string CicleShrink = "circle_shrink";
            public static string CircleUngrow = "circle_ungrow";
            public static string CircleUnshrink = "circle_unshrink";
        }

        public static class Pallette
        {
            public static Color MainMenuBlue = new(72, 158, 255, 255);
            public static Color MainMenuLightBlue = new(175, 245, 255, 255);

            public static Color NearBlack = new(15, 15, 15, 255);
        }
    }
}
