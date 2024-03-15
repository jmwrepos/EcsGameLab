using EcsGameLib;
using Microsoft.Xna.Framework;

namespace EcsGameLab.Components
{
    public abstract class Component
    {
        public string Name { get; set; }
        public GameObject Owner { get; set; }
        public bool Expires { get; set; }
        public bool IsExpired { get; set; }
        public Component(bool expires)
        {
            Expires = expires;
        }

        // Initialize the component.
        public virtual void Initialize() { }

        // Terminate the component.
        public virtual void Terminate()
        {
            if (Expires)
            {
                IsExpired = true;
            }
        }

        // Update the component.
        public virtual void Update(GameTime gameTime) { }
    }
}
