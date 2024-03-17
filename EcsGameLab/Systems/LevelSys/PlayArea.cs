using EcsGameLab.Components;
using EcsGameLab.Statics;
using EcsGameLib;
using Microsoft.Xna.Framework;

namespace EcsGameLab.Systems.LevelSys
{
    public class PlayArea : GameObject
    {
        public PlayArea()
        {
            //var display = GraphicsLib.GetDisplaySize;
            //var design = GraphicsLib.DesignSize;
            //var xScale = display.X / design.X;
            //var yScale = display.Y / design.Y;
            //var x = 50 * xScale;
            //var y = 50 * yScale;
            //var width = (display.X - 100) * xScale;
            //var height = (display.Y - 100) * yScale;
            //Rectangle playArea = new Rectangle((int)x, (int)y, (int)width, (int)height);

            Rectangle playArea = GraphicsLib.GetScaledAndCenteredRectangle(1800, 980);

            TransformComponent transform = new TransformComponent();
            ColorComponent color = new ColorComponent(GraphicsLib.Pallette.NearBlack);
            AddComponent(color);
            transform.UpdateBounds(playArea);
            AddComponent(transform);
            TextureComponent texture = new("playArea", GraphicsLib.Pixel);
            AddComponent(texture);
        }
    }
}
