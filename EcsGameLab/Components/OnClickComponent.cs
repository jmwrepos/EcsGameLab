using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcsGameLab.Components
{
    public class OnClickComponent : Component
    {
        public bool Clicked { get; set; }
        public OnClickComponent(string name, bool expires = false) : base(expires)
        {
            Name = name;
        }
    }
}
