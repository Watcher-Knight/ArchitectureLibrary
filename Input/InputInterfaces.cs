using UnityEngine;

namespace ArchitectureLibrary
{
    public interface IAxisControllable { void Control(float axis); }
    public interface IAxisControllable2D { void Control(Vector2 axis); }
}
