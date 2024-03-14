using EcsGameLab.Components;
using EcsGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EcsGameLab.Systems.MainMenuSys
{
    public class MainMenuSystem
    {
        private readonly GraphicsDeviceManager _gdm;
        public List<GameObject> Entities { get; set; }
        private int DisplayWidth { get; set; }
        private int DisplayHeight { get; set; }
        private int RenderOrder { get; set; } = 0;
        public MainMenuSystem(GraphicsDeviceManager gdm)
        {
            _gdm = gdm;
        }

        public void LoadContent(ContentManager content)
        {
            DisplayWidth = _gdm.PreferredBackBufferWidth;
            DisplayHeight = _gdm.PreferredBackBufferHeight;
            var graphics = _gdm.GraphicsDevice;

            Texture2D pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new[] { Color.White });

            Texture2D titleTexture = content.Load<Texture2D>("statics/mainMenuNew");
            var object1 = CreateGameObject("obj1", pixel, 0, Color.Transparent, Color.Black, Vector2.Zero, new(DisplayWidth, DisplayHeight));
            var object2 = CreateGameObject("obj2", pixel, 1, Color.Transparent, Color.Yellow, new(25,25), new(DisplayWidth - 50, DisplayHeight - 50));
            var title = CreateGameObject("title", titleTexture, 2, Color.Transparent, Color.Black);

            title.AddComponent(new AlignmentComponent(0.1f, 0.5f, _gdm));
            Entities = new() { object1, object2, title};

            SetStartAfter(object1, object2);
            SetStartAfter(object2, title);
        }

        private void SetStartAfter(GameObject obj1, GameObject obj2) =>
            obj2.GetComponent<AnimationComponent>().SetStartAfter(
                obj1.GetComponent<AnimationComponent>());

        public GameObject CreateGameObject(string name, Texture2D texture, int renderOrder, Color startColor, Color endColor)
        {
            return CreateGameObject(name, texture, renderOrder, startColor, endColor, Vector2.Zero, new(texture.Width, texture.Height));
        }
        public GameObject CreateGameObject(string name, Texture2D texture, int renderOrder, Color startColor, Color endColor, Vector2 position, Vector2 size)
        {
            var obj = new GameObject() { Name = name };
            TextureComponent txt = new(_gdm.GraphicsDevice, texture);
            FadeAnimationComponent fadeIn = new(startColor, endColor, 0.61, renderOrder);
            ColorComponent clr = new(startColor);

            // Ensure that Bounds is properly initialized with position and size
            TransformComponent trans = new();
            trans.Size = new(size.X, size.Y);
            trans.Position = new(position.X, position.Y);

            obj.AddComponent(txt);
            obj.AddComponent(fadeIn);
            obj.AddComponent(clr);
            obj.AddComponent(trans);
            return obj;
        }

        private List<Component> ToDestroy = new();
        public void Update(GameTime gameTime)
        {
            ToDestroy.Clear();
            foreach(GameObject obj in Entities)
            {
                foreach(Component comp  in obj.Components)
                {
                    if(comp is AnimationComponent)
                    {
                        UpdateAnimations(obj, gameTime);
                    }
                    else
                    {
                        comp.Update(gameTime);
                    }
                    if (comp.Destroy)
                    {
                        ToDestroy.Add(comp);
                    }
                }
            }
            foreach(Component toDestroy in ToDestroy)
            {
                toDestroy.Owner.Components.Remove(toDestroy);
            }
        }

        private void UpdateAnimations(GameObject obj, GameTime gameTime)
        {
            var fade = obj.GetComponent<FadeAnimationComponent>();
            if (!fade.HasFinished && fade.RenderOrder == RenderOrder)
            {
                fade.Update(gameTime);
                if (fade.HasFinished)
                {
                    RenderOrder++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(GameObject obj in Entities)
            {
                DrawGameObject(spriteBatch, obj);
            }
        }

        private void DrawGameObject(SpriteBatch spriteBatch, GameObject gameObject)
        {
            var textureComponent = gameObject.GetComponent<TextureComponent>();
            var colorComponent = gameObject.GetComponent<ColorComponent>();
            var transformComponent = gameObject.GetComponent<TransformComponent>();

            if (textureComponent != null && colorComponent != null && transformComponent != null)
            {
                // Use the Bounds property for drawing which includes both position and size,
                // allowing the texture to be resized and stretched accordingly
                spriteBatch.Draw(
                    textureComponent.Texture,
                    destinationRectangle: transformComponent.Bounds,
                    color: colorComponent.Color
                );
            }
        }

    }
}
