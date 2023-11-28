using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName = "StringVariable", menuName = "Variable/String", order = 0)]
public class StringVariable : Variable
{
    public string value = "Add Text";

    public new string ToString() => value;
}