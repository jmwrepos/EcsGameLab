using EcsGameLab.Components;
using EcsGameLab.Systems.LevelSys.Levels;
using EcsGameLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EcsGameLab.Systems.LevelSys
{
    public class LevelSystem : ISystem
    {
        public bool Active { get; set; }
        //the level system contains entities related to the level the player is on.
        //currently I am only testing the 1 level, and just the border/collision
        public List<GameObject> Entities { get; set; }
        public GameObject CurrentLevel { get; set; }
        public bool Quit { get; set; }
        public LevelSystem()
        {
            Entities = new List<GameObject>();
        }
        public void LoadContent()
        {
            var lvl1 = new Level1();
            Entities.Add(lvl1);
            CurrentLevel = lvl1;

            var level1Border = new LevelBorder();
            Entities.Add(level1Border);

            var playArea = new PlayArea();
            Entities.Add(playArea);

            AddNodes(playArea);
        }

        private void AddNodes(PlayArea playArea)
        {
            int nodeSize = 35; // Size of each node
            Rectangle area = playArea.GetComponent<TransformComponent>().Bounds; // The area to fill with nodes

            int nodesAcross = area.Width / nodeSize; // Calculate how many nodes fit horizontally
            int nodesDown = area.Height / nodeSize; // Calculate how many nodes fit vertically

            for (int x = 0; x < nodesAcross; x++)
            {
                for (int y = 0; y < nodesDown; y++)
                {
                    // Calculate the position of the current node
                    Vector2 position = new Vector2(area.X + x * nodeSize, area.Y + y * nodeSize);

                    // Instantiate the node. Assuming CrossNode takes a Vector2 for position and an int for size.
                    var crossNode = new CrossNode(position, nodeSize);

                    // Add the node to the list of entities
                    Entities.Add(crossNode);
                }
            }
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
                    foreach (var comp in item.Components)
                    {
                        if (comp is TextureComponent component)
                        {
                            var texture = component.Texture;
                            var transform = item.GetComponent<TransformComponent>();
                            var color = item.GetComponent<ColorComponent>();
                            spriteBatch.Draw(texture, transform.Bounds, color.Color);
                        }
                    }
                }
            }
        }


    }
}
