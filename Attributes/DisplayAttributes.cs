using System;

namespace ArchitectureLibrary
{
    public class DisplayAttribute : Attribute { }
    public class DisplayPlayModeAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Class)] public class UpdateEditorAttribute : Attribute { }
}