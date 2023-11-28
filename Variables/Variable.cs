using UnityEngine;

public abstract class Variable : ScriptableObject
{
    public new virtual string ToString()
    {
        string errorMessage = "Cannot convert to string";
        Debug.LogError(errorMessage);
        return errorMessage;
    }
}

public enum Comparison
{
    EqualTo,
    LessThan,
    GreaterThan,
    LessThanOrEqualTo,
    GreaterThanOrEqualTo
}