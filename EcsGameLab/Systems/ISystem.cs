using EcsGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EcsGameLab.Systems
{
    public interface ISystem
    {
        public bool Active { get; set; }
        List<GameObject> Entities { get; set; }
        bool Quit { get; set; }
        void LoadContent();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}