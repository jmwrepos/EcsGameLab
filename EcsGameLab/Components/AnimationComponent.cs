using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace EcsGameLab.Components
{
    public class AnimationComponent : Component
    {
        private List<Rectangle> frames; // Frames of the animation
        private int currentFrame;
        private double timePerFrame; // Seconds per frame
        private double timeSinceLastFrame;
        private bool hasFinished; // Indicates whether the animation has finished

        public Texture2D Texture { get; private set; } // The sprite sheet

        // Public property to check if the animation has finished
        public bool HasFinished
        {
            get { return hasFinished; }
            private set { hasFinished = value; }
        }

        public AnimationComponent(Texture2D texture, int frameCount, double animationSpeed)
        {
            Texture = texture;
            frames = new List<Rectangle>();
            hasFinished = false; // Initially, the animation has not finished

            // Assuming all frames are in a single row and equally spaced
            int frameWidth = texture.Width / frameCount;
            for (int i = 0; i < frameCount; i++)
            {
                frames.Add(new Rectangle(i * frameWidth, 0, frameWidth, texture.Height));
            }

            timePerFrame = 1.0 / animationSpeed;
        }

        public void Update(GameTime gameTime)
        {
            if (!hasFinished)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastFrame >= timePerFrame)
                {
                    currentFrame = (currentFrame + 1) % frames.Count; // Loop through the frames
                    timeSinceLastFrame -= timePerFrame;

                    // If we've looped back to the first frame, mark the animation as finished
                    if (currentFrame == 0)
                    {
                        hasFinished = true;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!hasFinished)
            {
                spriteBatch.Draw(Texture, Owner.GetComponent<TransformComponent>().Position, frames[currentFrame], Color.White);
            }
        }

        // Method to reset the animation
        public void Reset()
        {
            currentFrame = 0;
            timeSinceLastFrame = 0;
            hasFinished = false;
        }
    }
}