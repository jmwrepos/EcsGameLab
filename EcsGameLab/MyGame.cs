using EcsGameLab.Statics;
using EcsGameLab.Systems;
using EcsGameLab.Systems.LevelSys;
using EcsGameLab.Systems.MainMenuSys;
using EcsGameLab.Systems.PlayerSys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EcsGameLab
{
    public class MyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<ISystem> Systems = new();

        private MainMenuSystem _mainMenuSystem;
        private LevelSystem _levelSystem;
        private PlayerSystem _playerSystem;

        public void StartGame()
        {
            _mainMenuSystem.Active = false;
            _levelSystem.Active = true;
            _playerSystem.Active = true;
        }
        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = (int)(GraphicsLib.DesignSize.X * 1.5);
            _graphics.PreferredBackBufferHeight = (int)(GraphicsLib.DesignSize.Y * 1.5);


            _mainMenuSystem = new MainMenuSystem(this);
            Systems.Add(_mainMenuSystem);

            _levelSystem = new LevelSystem();
            Systems.Add(_levelSystem);

            _playerSystem = new PlayerSystem();
            Systems.Add(_playerSystem);

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
            _levelSystem.LoadContent();
            _playerSystem.LoadContent();
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach(ISystem system in Systems)
            {
                system.Update(gameTime);
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // Start the SpriteBatch with alpha blending enabled
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            foreach(ISystem system in Systems)
            {
                system.Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}