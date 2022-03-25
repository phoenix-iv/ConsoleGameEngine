using ConsoleGameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Cameras
{
    public class Camera
    {
        private Position _position;
        public ref Position Position { get => ref _position; }
    }
}
