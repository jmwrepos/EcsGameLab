using EcsGameLab.Components;
using EcsGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EcsGameLab.Systems.PlayerSys
{
    public class PlayerSystem : ISystem
    {
        public bool Active { get; set;}
        public List<GameObject> Entities { get; set; } = new();
        public bool Quit { get; set; }

        public void LoadContent()
        {
            PlayerChar player1 = new PlayerChar(PlayerIndex.One);
            Entities.Add(player1);
        }

        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                foreach (var item in Entities)
                {
                    foreach (var comp in item.Components)
                    {
                        comp.Update(gameTime);
                    }
                }

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                foreach (var item in Entities)
                {
                    // Assuming there's a TransformComponent and a TextureComponent in every drawable entity
                    TransformComponent transform = item.GetComponent<TransformComponent>();
                    TextureComponent textureComp = item.GetComponent<TextureComponent>();
                    ColorComponent colorComp = item.GetComponent<ColorComponent>();

                    if (textureComp != null && transform != null)
                    {
                        var texture = textureComp.Texture;
                        var color = colorComp != null ? colorComp.Color : Color.White; // Use white color if no ColorComponent is found

                        // Calculate the origin of rotation as the center of the texture
                        Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

                        // Convert the Bounds (Rectangle) to a Vector2 for the position
                        // Note: This position will be the top-left corner of the rectangle
                        Vector2 position = new Vector2(transform.Bounds.X, transform.Bounds.Y) + origin; // Adjust position to account for rotation origin

                        // Use a modified Draw call that includes rotation and uses the calculated origin
                        spriteBatch.Draw(
                            texture: texture,
                            position: position,
                            sourceRectangle: null, // Use the entire texture
                            color: color,
                            rotation: transform.Rotation,
                            origin: origin,
                            scale: transform.Scale,
                            effects: SpriteEffects.None,
                            layerDepth: 0f
                        );
                    }
                }
            }
        }

    }
}
