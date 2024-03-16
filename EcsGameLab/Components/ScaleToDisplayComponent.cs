using EcsGameLab.Statics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcsGameLab.Components
{
    public class ScaleToDisplayComponent : Component
    {
        public ScaleToDisplayComponent(bool expires = true) : base(expires)
        {

        }

        public override void Update(GameTime gameTime)
        {
            var transform = Owner.GetComponent<TransformComponent>();
            Vector2 scaler = GraphicsLib.GetDisplaySize / GraphicsLib.DesignSize;
            transform.Size = new(transform.Size.X * scaler.X, transform.Size.Y * scaler.Y);
            Terminate();
            base.Update(gameTime);
        }
    }
}
