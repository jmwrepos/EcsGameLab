using EcsGameLab.Components;
using EcsGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EcsGameLab.Systems.MainMenuSys
{
    public class MainMenuSystem
    {
        private readonly GraphicsDeviceManager _gdm;
        public MainMenuState State { get; set; }

        //main menu objects
        public GameObject Object1 { get; set; }
        public GameObject Object2 { get; set; }
        private int DisplayWidth { get; set; }
        private int DisplayHeight { get; set; }
        public MainMenuSystem(GraphicsDeviceManager gdm)
        {
            _gdm = gdm;
            DisplayWidth = gdm.PreferredBackBufferWidth;
            DisplayHeight = gdm.PreferredBackBufferHeight;
            State = new();
        }

        public void LoadContent(ContentManager content)
        {
            var graphics = _gdm.GraphicsDevice;

            Texture2D pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new[] { Color.White });

            Object1 = CreateGameObject("obj1", pixel, 0, Color.Transparent, Color.Black, Vector2.Zero, new(DisplayWidth, DisplayHeight));
            Object2 = CreateGameObject("obj2", pixel, 1, Color.Transparent, Color.Yellow, new(25,25), new(DisplayWidth - 50, DisplayHeight - 50));
            var fadeAnimationComponent = Object1.GetComponent<FadeAnimationComponent>();
            if (fadeAnimationComponent != null)
            {
                fadeAnimationComponent.StartAnimation(new GameTime()); // Pass a valid GameTime instance
            }
        }

        public GameObject CreateGameObject(string name, Texture2D texture, int renderOrder, Color startColor, Color endColor, Vector2 position, Vector2 size)
        {
            var obj = new GameObject() { Name = name };
            TextureComponent txt = new(_gdm.GraphicsDevice, texture, size);
            FadeAnimationComponent fadeIn = new(startColor, endColor, 2.5, renderOrder);
            ColorComponent clr = new(startColor);
            TransformComponent trans = new() { Position = position, Size = size };
            obj.AddComponent(txt);
            obj.AddComponent(fadeIn);
            obj.AddComponent(clr);
            obj.AddComponent(trans);
            return obj;
        }
        public void Update(GameTime gameTime)
        {
            // Assuming only Object1 and Object2 for simplicity
            switch (State.CurrentRenderOrder)
            {
                case 0:
                    var fade1 = Object1.GetComponent<FadeAnimationComponent>();
                    if (!fade1.HasFinished)
                    {
                        fade1.Update(gameTime);
                    }
                    else
                    {
                        State.CurrentRenderOrder = 1; // Move to next render order
                        var fade2 = Object2.GetComponent<FadeAnimationComponent>();
                        fade2.StartAnimation(gameTime); // Explicitly start animation for Object2
                    }
                    break;
                case 1:                    
                    var fade2b = Object2.GetComponent<FadeAnimationComponent>();
                    if (!fade2b.HasFinished)
                    {
                        fade2b.Update(gameTime);
                    }
                    break;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Object1 if its FadeAnimationComponent has started
            var fade1 = Object1.GetComponent<FadeAnimationComponent>();
            if (fade1.StartTime >= 0) // Checks if the animation has started
            {
                DrawGameObject(spriteBatch, Object1);
            }

            // Draw Object2 if its FadeAnimationComponent has started
            var fade2 = Object2.GetComponent<FadeAnimationComponent>();
            if (fade2.StartTime >= 0) // Checks if the animation has started
            {
                DrawGameObject(spriteBatch, Object2);
            }
        }

        private void DrawGameObject(SpriteBatch spriteBatch, GameObject gameObject)
        {
            var textureComponent = gameObject.GetComponent<TextureComponent>();
            var colorComponent = gameObject.GetComponent<ColorComponent>();
            var transformComponent = gameObject.GetComponent<TransformComponent>();
            var fadeComp = gameObject.GetComponent<FadeAnimationComponent>();

            if (textureComponent != null && colorComponent != null && transformComponent != null)
            {
                // Draw the texture with the current color (which includes the fade effect) and position
                spriteBatch.Draw(textureComponent.Texture, position: transformComponent.Position, color: colorComponent.Color);
            }
        }
    }
}
