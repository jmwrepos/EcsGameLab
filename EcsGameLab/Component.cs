using EcsGameLib;

namespace EcsGameLab
{
    public abstract class Component
    {
        public GameObject Owner { get; set; }
    }
}