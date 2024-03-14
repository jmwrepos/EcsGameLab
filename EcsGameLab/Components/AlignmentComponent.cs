using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public class AlignmentComponent : Component
    {
        //scale 0 to 1 for % of display height / width
        public float Vertical { get; set; }
        public float Horizontal { get; set; }
        private GraphicsDeviceManager _gdm;

        public AlignmentComponent(float v, float h, GraphicsDeviceManager gdm)
        {
            _gdm = gdm;
            Vertical = v;
            Horizontal = h;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Destroy)
            {
                var transform = Owner.GetComponent<TransformComponent>();
                if (transform == null) return; // Ensure the transform component exists

                float ownerWidth = transform.Bounds.Width;
                float ownerHeight = transform.Bounds.Height;
                float displayWidth = _gdm.PreferredBackBufferWidth;
                float displayHeight = _gdm.PreferredBackBufferHeight;

                // Calculate new positions
                float newX = Horizontal * (displayWidth - ownerWidth);
                float newY = Vertical * (displayHeight - ownerHeight);

                // Set the new position, aligning based on the calculations above
                // The position will adjust so that the object aligns according to the specified percentages
                transform.Position = new(newX, newY);
                // Optional: Terminate this component if its job is done and it's no longer needed
                Terminate();
            }
        }

    }
}
