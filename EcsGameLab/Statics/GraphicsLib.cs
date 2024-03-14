using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EcsGameLab.Statics
{
    public static class GraphicsLib
    {
        private static ContentManager _content;
        private static GraphicsDeviceManager _graphics;
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
        }
        
        //ANIMATIONS
        public static class AnimationPaths
        {
            public static string CircleGrow = "circle_grow";
            public static string CicleShrink = "circle_shrink";
            public static string CircleUngrow = "circle_ungrow";
            public static string CircleUnshrink = "circle_unshrink";
        }
    }
}
