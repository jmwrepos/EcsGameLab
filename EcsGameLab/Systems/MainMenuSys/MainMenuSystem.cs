using EcsGameLab.Components;
using EcsGameLab.Statics;
using EcsGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static EcsGameLab.Statics.GraphicsLib;

namespace EcsGameLab.Systems.MainMenuSys
{
    public class MainMenuSystem
    {
        public List<GameObject> Entities { get; set; }
        private int DisplayWidth { get; set; }
        private int DisplayHeight { get; set; }
        public bool Quit { get; set; }
        public void LoadContent()
        {
            //set display properties
            var display = GetDisplaySize;
            DisplayWidth = (int)display.X;
            DisplayHeight = (int)display.Y;


            //load textured background
            var textureRect = GetStatic("mainMenuTextureRect");
            //load textures for subsequent sections:
            var titleTexture = MakeTextureFromText(FontPaths.MainMenuTitle, GlobalMisc.GameTitle, Color.White, Color.Transparent);
            var newTexture = MakeTextureFromText(FontPaths.MainMenuOption, GlobalMisc.BtnMainMenuNew, Color.White, Color.Transparent);
            var continueTexture = MakeTextureFromText(FontPaths.MainMenuOption, GlobalMisc.BtnMainMenuCont, Color.White, Color.Transparent);
            var quitTexture = MakeTextureFromText(FontPaths.MainMenuOption, GlobalMisc.BtnMainMenuQuit, Color.White, Color.Transparent);

            //create objects
            var bg1 = CreateGameObject(ObjectNames.MainMenuBackground, Pixel, 0, Color.Transparent, Color.Black, Vector2.Zero, new(DisplayWidth, DisplayHeight));
            var bg2 = CreateGameObject(ObjectNames.MainMenuBackground2, textureRect, 1, Color.Transparent, Pallette.MainMenuBlue, new(25,25), new(DisplayWidth - 50, DisplayHeight - 50));
            var title = CreateGameObject(ObjectNames.MainMenuTitle, titleTexture, 2, Color.Transparent, Pallette.MainMenuLightBlue);
            var newG = CreateGameObject(ObjectNames.MainMenuOptNew, newTexture, 2, Color.Transparent, Pallette.MainMenuLightBlue);
            var contG = CreateGameObject(ObjectNames.MainMenuOptCont, continueTexture, 2, Color.Transparent, Pallette.MainMenuLightBlue);
            var quitG = CreateGameObject(ObjectNames.MainMenuOptQuit, quitTexture, 2, Color.Transparent, Pallette.MainMenuLightBlue);
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

            //display scaling
            title.AddComponent(new ScaleToDisplayComponent());
            newG.AddComponent(new ScaleToDisplayComponent());
            contG.AddComponent(new ScaleToDisplayComponent());
            quitG.AddComponent(new ScaleToDisplayComponent());

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
            FadeAnimationComponent mouseIn = new FadeAnimationComponent(AnimationNames.MouseIn, Pallette.MainMenuLightBlue, Color.Cyan, 0.15f);
            FadeAnimationComponent mouseOut = new FadeAnimationComponent(AnimationNames.MouseOut, Color.Cyan, Pallette.MainMenuLightBlue, 0.15f);
            FadeAnimationComponent mouseLeftDown = new FadeAnimationComponent(AnimationNames.MouseLeftDown, Color.Cyan, Color.DarkCyan, 0.15f);
            FadeAnimationComponent mouseLeftUp = new FadeAnimationComponent(AnimationNames.MouseLeftUp, Color.DarkCyan, Color.Cyan, 0.15f);
            MouseInteractionComponent mouseInteraction = new MouseInteractionComponent();


            obj.AddComponent(mouseIn);
            obj.AddComponent(mouseOut);
            obj.AddComponent(mouseLeftDown);
            obj.AddComponent(mouseLeftUp);
            obj.AddComponent(mouseInteraction);

            OnClickComponent onClick = new(obj.Name);
            obj.AddComponent(onClick);
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
                    if(comp is OnClickComponent)
                    {
                        var onClick = ((OnClickComponent)comp);
                        if (onClick.Clicked)
                        {
                            switch (onClick.Name)
                            {
                                case ObjectNames.MainMenuOptQuit:
                                    Quit = true;
                                    break;
                                default:
                                    break;
                            }
                        }
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
                spriteBatch.Draw(
                    textureComponent.Texture,
                    destinationRectangle: transformComponent.Bounds,
                    color: colorComponent.Color
                );
            }
        }

    }
}
