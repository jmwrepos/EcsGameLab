using EcsGameLab.Components;
using EcsGameLab.Statics;
using EcsGameLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EcsGameLab.Systems.LevelSys.Levels
{
    public class Level1 : GameObject
    {
        public Level1()
        {
            Name = "Level1";
            TextComponent description = new("description","Move your character into the box.");
            TextComponent onWin = new("onWin", "Move your character into the box.");
            AddComponent(description);
            AddComponent(onWin);
            var display = GraphicsLib.GetDisplaySize;
            TransformComponent trans = new TransformComponent() { Size = new(display.X, display.Y), Position = Vector2.Zero };
            AddComponent(trans);
        }
    }
}
