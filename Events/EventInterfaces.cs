using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    public interface IInvokeable { void Invoke(); }
    public interface ICancellable { void Cancel(); }
}