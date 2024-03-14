using EcsGameLib;
using Microsoft.Xna.Framework;

namespace EcsGameLab
{
    public abstract class Component
    {
        public GameObject Owner { get; set; }
        public bool Destroy { get; set; }

        // Initialize the component.
        public virtual void Initialize() { }

        // Terminate the component.
        public virtual void Terminate() {
            Destroy = true;
        }

        // Update the component.
        public virtual void Update(GameTime gameTime) { }
    }
}
