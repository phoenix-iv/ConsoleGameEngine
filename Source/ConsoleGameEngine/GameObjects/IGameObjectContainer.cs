using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.GameObjects
{
    public interface IGameObjectContainer
    {
        void AddChild(GameObject gameObject);
        void RemoveChild(GameObject gameObject);
    }
}
