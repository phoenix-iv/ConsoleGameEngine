using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.GameObjects
{
    public class GameObject
    {
        public Entity Entity { get; }

        public GameObject(Entity entity)
        {
            Entity = entity;
        }
    }
}
