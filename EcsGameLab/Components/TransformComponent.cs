using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public class TransformComponent : Component
    {
        private Vector2 _position;
        private Vector2 _size;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateBounds();
            }
        }

        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                UpdateBounds();
            }
        }

        public Rectangle Bounds { get; private set; }
        public float Rotation { get; set; }
        public float Radius { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;

        private void UpdateBounds()
        {
            Bounds = new Rectangle((int)_position.X, (int)_position.Y, (int)_size.X, (int)_size.Y);
        }
    }

}
