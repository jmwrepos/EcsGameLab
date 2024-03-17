using EcsGameLab.Components;
using EcsGameLab.Statics;
using EcsGameLib;
using Microsoft.Xna.Framework;

namespace EcsGameLab.Systems.LevelSys
{
    public class CrossNode : GameObject
    {
        public CrossNode(Vector2 position, int size)
        {
            TransformComponent transform = new TransformComponent();
            Rectangle rect = new((int)(position.X), (int)(position.Y),(size), (size));
            transform.UpdateBounds(rect);
            ColorComponent color = new(Color.White);
            TextureComponent texture = new("node",GraphicsLib.GetStatic("node001"));

            AddComponent(color);
            AddComponent(texture);
            AddComponent(transform);
        }
    }
}
