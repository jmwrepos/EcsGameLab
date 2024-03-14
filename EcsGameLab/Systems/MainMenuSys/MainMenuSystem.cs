using EcsGameLab.Components;
using EcsGameLab.Statics;
using EcsGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EcsGameLab.Systems.MainMenuSys
{
    public class MainMenuSystem
    {
        public List<GameObject> Entities { get; set; }
        private int DisplayWidth { get; set; }
        private int DisplayHeight { get; set; }
        public void LoadContent()
        {
            //set display propertie
            var display = GraphicsLib.GetDisplaySize;
            DisplayWidth = (int)display.X;
            DisplayHeight = (int)display.Y;

            //load textures for subsequent sections:
            var titleTexture = GraphicsLib.MakeTextureFromText("gameTitle", "These Levels", Color.Black, Color.Transparent);

            //create objects
            var object1 = CreateGameObject("obj1", GraphicsLib.Pixel, 0, Color.Transparent, Color.Black, Vector2.Zero, new(DisplayWidth, DisplayHeight));
            var object2 = CreateGameObject("obj2", GraphicsLib.Pixel, 1, Color.Transparent, Color.Yellow, new(25,25), new(DisplayWidth - 50, DisplayHeight - 50));
            var title = CreateGameObject("title", titleTexture, 2, Color.Transparent, Color.Black);
            var animationMan = new GameObject();

            //create animation tree component to manage the menu fade in
            var animationTree = new AnimationTreeComponent(true);
            animationMan.AddComponent(animationTree);

            //
            var a1 = object1.GetComponent<FadeAnimationComponent>();
            var a2 = object2.GetComponent<FadeAnimationComponent>();
            var a3 = title.GetComponent<FadeAnimationComponent>();

            animationTree.AddAnimationNode(a1);
            animationTree.AddAnimationNode(a2, a1);
            animationTree.AddAnimationNode(a3, a2);

            title.AddComponent(new AlignmentComponent(true, 0.1f, 0.5f));
            Entities = new() { object1, object2, title, animationMan };
        }


        public GameObject CreateGameObject(string name, Texture2D texture, int renderOrder, Color startColor, Color endColor)
        {
            return CreateGameObject(name, texture, renderOrder, startColor, endColor, Vector2.Zero, new(texture.Width, texture.Height));
        }
        public GameObject CreateGameObject(string name, Texture2D texture, int renderOrder, Color startColor, Color endColor, Vector2 position, Vector2 size)
        {
            var obj = new GameObject() { Name = name };
            TextureComponent txt = new(texture);
            FadeAnimationComponent fadeIn = new(Naming.FadeIn, startColor, endColor, 0.61);
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
                    if (comp.IsExpired)
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
            if(fade.HasStarted && !fade.HasFinished)
            {
                fade.Update(gameTime);
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
                spriteBatch.Draw(
                    textureComponent.Texture,
                    destinationRectangle: transformComponent.Bounds,
                    color: colorComponent.Color
                );
            }
        }

    }
}
