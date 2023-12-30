using System;
using UnityEngine;

namespace ArchitectureLibrary
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute : Attribute
    {
        public string name { get; private set; }
        public string description { get; private set; }

        public TestAttribute(string name) => this.name = name;
        public TestAttribute(string name, string description) : this(name) => this.description = description;
    }
}