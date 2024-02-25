namespace ArchitectureLibrary
{
    public struct ObjectTag
    {
        // String reference: ObjectTagDrawer
        public string Name;

        public static bool operator ==(ObjectTag tag1, ObjectTag tag2) => tag1.Name == tag2.Name;
        public static bool operator !=(ObjectTag tag1, ObjectTag tag2) => tag1.Name != tag2.Name;
        public static bool operator ==(ObjectTag tag, string text) => tag.Name == text;
        public static bool operator !=(ObjectTag tag, string text) => tag.Name != text;
        public static bool operator ==(string text, ObjectTag tag) => tag.Name == text;
        public static bool operator !=(string text, ObjectTag tag) => tag.Name != text;
        public static implicit operator string(ObjectTag tag) => tag.Name;
        public override bool Equals(object obj) =>
            obj switch
            {
                ObjectTag tag => tag.Name == Name,
                string text => text == Name,
                _ => false
            };
        public override int GetHashCode() => Name.GetHashCode();
        public override string ToString() => Name;
    }
}