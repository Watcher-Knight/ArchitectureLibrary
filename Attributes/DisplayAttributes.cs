using System;

namespace ArchitectureLibrary
{
    public class DisplayAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Method)] public class ButtonAttribute : Attribute { }
    public class HideAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Class)] public class DisplayPropertiesAttribute : Attribute { }
    public class DisplayPlayModeAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Class)] public class UpdateEditorAttribute : Attribute { }
}