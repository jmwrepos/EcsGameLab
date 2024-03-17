using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input; // Include this to access GamePad functionalities
using System;

namespace EcsGameLab.Components
{
    public class PlayerControllerComponent : Component
    {
        public PlayerIndex PlayerIndex { get; set; }

        public PlayerControllerComponent(PlayerIndex plIndex, bool expires = true) : base(expires)
        {
            PlayerIndex = plIndex;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            TransformComponent playerTransform = Owner.GetComponent<TransformComponent>();
            VelocityComponent velocityComponent = Owner.GetComponent<VelocityComponent>();

            // Read the current state of the GamePad for the assigned player index
            GamePadState state = GamePad.GetState(PlayerIndex);

            // Assuming you want to move the player based on the left thumbstick's position
            Vector2 thumbstick = state.ThumbSticks.Left;

            // Convert the thumbstick input into a velocity vector, adjust the scale as needed
            Vector2 newVelocity = new Vector2(thumbstick.X, -thumbstick.Y) * 100; // Example scaling factor

            // Only update velocity and rotation if there's actual input
            if (newVelocity != Vector2.Zero)
            {
                velocityComponent.Velocity = newVelocity;

                // Update the player's position based on the velocity
                playerTransform.Position += velocityComponent.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Calculate and update the rotation of the player to face the direction of movement
                // Math.Atan2 returns the angle in radians, so no conversion is necessary for use in XNA/MonoGame
                playerTransform.Rotation = (float)Math.Atan2(velocityComponent.Velocity.Y, velocityComponent.Velocity.X);
            }

            // Additional logic for collision detection and handling can be placed here
        }
    }
}
