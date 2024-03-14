using EcsGameLab.Statics;
using EcsGameLab.Systems.MainMenuSys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EcsGameLab
{
    public class MyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MainMenuSystem _mainMenuSystem;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _mainMenuSystem = new();

            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //setters
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsLib.SetManagers(Content, _graphics);
            GraphicsLib.SetPixel();
            //loading
            _mainMenuSystem.LoadContent();
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _mainMenuSystem.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Start the SpriteBatch with alpha blending enabled
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            _mainMenuSystem.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}