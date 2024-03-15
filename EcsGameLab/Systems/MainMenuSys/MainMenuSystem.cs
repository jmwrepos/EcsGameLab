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
            var titleTexture = GraphicsLib.MakeTextureFromText("gameTitle", "These Levels", Color.White, Color.Transparent);
            var newTexture = GraphicsLib.MakeTextureFromText("mainMenuOption", "New", Color.White, Color.Transparent);
            var continueTexture = GraphicsLib.MakeTextureFromText("mainMenuOption", "Continue", Color.White, Color.Transparent);
            var quitTexture = GraphicsLib.MakeTextureFromText("mainMenuOption", "Quit", Color.White, Color.Transparent);

            //create objects
            var bg1 = CreateGameObject("bg1", GraphicsLib.Pixel, 0, Color.Transparent, Color.Black, Vector2.Zero, new(DisplayWidth, DisplayHeight));
            var bg2 = CreateGameObject("bg2", GraphicsLib.Pixel, 1, Color.Transparent, Color.Yellow, new(25,25), new(DisplayWidth - 50, DisplayHeight - 50));
            var title = CreateGameObject("title", titleTexture, 2, Color.Transparent, Color.Black);
            var newG = CreateGameObject("new", newTexture, 2, Color.Transparent, Color.Black);
            var contG = CreateGameObject("continue", continueTexture, 2, Color.Transparent, Color.Black);
            var quitG = CreateGameObject("quit", quitTexture, 2, Color.Transparent, Color.Black);
            var animationMan = new GameObject();

            //create animation tree component to manage the menu fade in
            var animationTree = new AnimationTreeComponent(true);
            animationMan.AddComponent(animationTree);

            //assign animations
            var a1 = bg1.GetComponent<FadeAnimationComponent>();
            var a2 = bg2.GetComponent<FadeAnimationComponent>();
            var a3 = title.GetComponent<FadeAnimationComponent>();
            var a4 = newG.GetComponent<FadeAnimationComponent>();
            var a5 = contG.GetComponent<FadeAnimationComponent>();
            var a6 = quitG.GetComponent<FadeAnimationComponent>();

            animationTree.AddAnimationNode(a1);
            animationTree.AddAnimationNode(a2, a1);
            animationTree.AddAnimationNode(a3, a2);
            animationTree.AddAnimationNode(a4, a2);
            animationTree.AddAnimationNode(a5, a2);
            animationTree.AddAnimationNode(a6, a2);

            //alignments
            title.AddComponent(new AlignmentComponent(true, 0.1f, 0.5f));
            newG.AddComponent(new AlignmentComponent(true, 0.5f, 0.5f));
            contG.AddComponent(new AlignmentComponent(true, 0.65f, 0.5f));
            quitG.AddComponent(new AlignmentComponent(true, 0.8f, 0.5f));


            //add button behavior:
            AddButtonBehavior(newG);
            AddButtonBehavior(contG);
            AddButtonBehavior(quitG);

            //set entities and initalize components
            Entities = new() { bg1, bg2, title, animationMan, newG, contG, quitG };
            InitializeComponents();
        }

        private void AddButtonBehavior(GameObject obj)
        {
            FadeAnimationComponent mouseIn = new FadeAnimationComponent(AnimationNames.MouseIn, Color.Black, Color.DarkRed, 0.15f);
            FadeAnimationComponent mouseOut = new FadeAnimationComponent(AnimationNames.MouseOut, Color.DarkRed, Color.Black, 0.15f);
            MouseInteractionComponent mouseInteraction = new MouseInteractionComponent();

            obj.AddComponent(mouseIn);
            obj.AddComponent(mouseOut);
            obj.AddComponent(mouseInteraction);
        }
        public GameObject CreateGameObject(string name, Texture2D texture, int renderOrder, Color startColor, Color endColor)
        {
            return CreateGameObject(name, texture, renderOrder, startColor, endColor, Vector2.Zero, new(texture.Width, texture.Height));
        }
        public GameObject CreateGameObject(string name, Texture2D texture, int renderOrder, Color startColor, Color endColor, Vector2 position, Vector2 size)
        {
            var obj = new GameObject() { Name = name };
            TextureComponent txt = new(texture);
            FadeAnimationComponent fadeIn = new(AnimationNames.FadeIn, startColor, endColor, 0.61);
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

        public void InitializeComponents()
        {
            foreach(var obj in Entities)
            {
                foreach(var comp in obj.Components)
                {
                    if(comp is MouseInteractionComponent)
                    {
                        comp.Initialize();
                    }
                }
            }
        }
        private List<Component> ToDestroy = new();
        public void Update(GameTime gameTime)
        {
            ToDestroy.Clear();
            foreach(GameObject obj in Entities)
            {
                foreach(Component comp  in obj.Components)
                {
                    comp.Update(gameTime);
                   
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
                EscLabLogger.Log($"Drawing {gameObject.Name} with color {colorComponent.Color}");
                spriteBatch.Draw(
                    textureComponent.Texture,
                    destinationRectangle: transformComponent.Bounds,
                    color: colorComponent.Color
                );
            }
        }

    }
}
