using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcsGameLab.Components
{
    public class TextComponent : Component
    {
        public string Value { get; set; }
        public TextComponent(string name, string value, bool expires = false) : base(expires)
        {
            Name = name;
            Value = value;
        }
    }
}
