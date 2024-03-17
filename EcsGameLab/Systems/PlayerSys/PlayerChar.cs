using EcsGameLab.Components;
using EcsGameLab.Statics;
using EcsGameLib;
using Microsoft.Xna.Framework;

namespace EcsGameLab.Systems.PlayerSys
{
    public class PlayerChar : GameObject
    {
        public PlayerChar(PlayerIndex playerIndex)
        {
            PlayerControllerComponent controller = new(playerIndex);
            AddComponent(controller);

            TextureComponent texture = new("player", GraphicsLib.GetStatic("dial"));
            AddComponent(texture);

            Vector2 middle = GraphicsLib.AbsMiddle;
            Rectangle playerStart = new Rectangle((int)middle.X - 17, (int)middle.Y - 17, 35, 35);
            TransformComponent transform = new();
            transform.UpdateBounds(playerStart);
            AddComponent(transform);

            ColorComponent color = new(Color.Cyan);
            AddComponent(color);

            VelocityComponent velocity = new VelocityComponent();
            AddComponent(velocity);

        }
    }
}
