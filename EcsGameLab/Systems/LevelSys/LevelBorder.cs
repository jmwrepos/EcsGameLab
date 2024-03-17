using EcsGameLab.Components;
using EcsGameLab.Statics;
using EcsGameLib;
using Microsoft.Xna.Framework;

namespace EcsGameLab.Systems.LevelSys
{
    public class LevelBorder : GameObject
    {
        public LevelBorder()
        {
            var display = GraphicsLib.GetDisplaySize;
            /*
            var design = GraphicsLib.DesignSize;
            var xScale = display.X / design.X;
            var yScale = display.Y / design.Y;
            var x = 50 * xScale;
            var y = 50 * yScale;
            var width = (display.X - 100) * xScale;
            var height = (display.Y - 100) * yScale;
            Rectangle playArea = new Rectangle((int)x, (int)y, (int)width, (int)height);*/

            Rectangle playArea = GraphicsLib.GetScaledAndCenteredRectangle(1800, 980);
            var collider = new ColliderComponent(true, playArea);
            AddComponent(collider);

            var transform = new TransformComponent() { Size = display };
            var texture = new TextureComponent("texture", GraphicsLib.GetStatic("lvl1Border"));
            var color = new ColorComponent(Color.White);
            AddComponent(transform);
            AddComponent(texture);
            AddComponent(color);
        }
    }
}
