using System;
using System.Linq;


namespace ArchitectureLibrary
{
    [Serializable]
    public struct EventTag
    {
        public string Name;
        public int Id;

        public static bool operator ==(EventTag a, EventTag b) => a.Id == b.Id;
        public static bool operator !=(EventTag a, EventTag b) => a.Id != b.Id;
        public override bool Equals(object obj) => Id == ((EventTag)obj).Id;
        public override int GetHashCode() => Id.GetHashCode();
    }
}