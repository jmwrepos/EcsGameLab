using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcsGameLab.Components
{
    public class PhysicsComponent : Component
    {
        public Vector2 Velocity { get; set; }
    }
}
