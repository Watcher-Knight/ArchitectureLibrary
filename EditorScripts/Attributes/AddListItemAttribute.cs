using System;

public class AddListItemAttribute : Attribute
{
    public string name;

    public AddListItemAttribute(string name) => this.name = name;
}