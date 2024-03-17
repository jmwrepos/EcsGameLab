using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using EcsGameLab.Components;
using System;

namespace EcsGameLib
{
    public class GameObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<Component> Components { get; private set; } = new List<Component>();

        public T GetComponent<T>() where T : Component
        {
            return Components.OfType<T>().FirstOrDefault();
        }
        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return Components.OfType<T>();
        }
        public void AddComponent(Component component)
        {
            component.Owner = this;
            Components.Add(component);
        }
    }
}