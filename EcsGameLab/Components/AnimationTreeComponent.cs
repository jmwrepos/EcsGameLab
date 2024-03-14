using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace EcsGameLab.Components
{
    public class AnimationTreeComponent : Component
    {
        private List<AnimationNode> nodes = new();

        public AnimationTreeComponent(bool expires) : base(expires)
        {

        }

        public void AddAnimationNode(AnimationComponent toAdd, AnimationComponent predecessor = null)
        {
            AnimationNode pre = null;
            if (predecessor != null)
            {
                foreach(AnimationNode entry in nodes)
                {
                    if(entry.Animation == predecessor)
                    {
                        pre = entry;
                        break;
                    }
                }
            }
            AnimationNode node = new AnimationNode()
            {
                Animation = toAdd,
                Predecessor = pre
            };
            nodes.Add(node);
        }

        public override void Update(GameTime gameTime)
        {
            foreach(AnimationNode node in nodes)
            {
                if (!node.Animation.HasStarted)
                {
                    bool start = node.Predecessor == null || node.Predecessor.Animation.HasFinished;
                    if (start)
                    {
                        node.Animation.StartAnimation(gameTime);
                    }
                }
            }
            base.Update(gameTime);
        }

        private class AnimationNode
        {
            public AnimationNode Predecessor { get; set; }
            public AnimationComponent Animation { get; set; }
        }
    }
}
