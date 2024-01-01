using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    public interface IInvokeable { void Invoke(); }
    public interface IInvokeable_1p { }
    public interface IInvokeable<T> : IInvokeable_1p { void Invoke(T p); }

    [Serializable]
    public class IInvokeableListItem
    {
        [SerializeField][RestrictTo(typeof(IInvokeable))] private UnityEngine.Object _item;
        public IInvokeable item => (IInvokeable)_item;
    }

    [Serializable]
    public class IInvokeableListItem<T>
    {
        [SerializeField][RestrictTo(typeof(IInvokeable_1p))] private UnityEngine.Object _item;
        public IInvokeable<T> item => (IInvokeable<T>)_item;
    }
}
