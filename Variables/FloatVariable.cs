using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variable/Float", order = 0)]
public class FloatVariable : Variable
{
    public float value = 0f;

    public void Set(float number)
    {
        value = number;
    }

    public void Add(float number)
    {
        value += number;
    }

    public override string ToString() => $"{value}";
}